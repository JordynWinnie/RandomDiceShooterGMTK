using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PointShopWaveSpawner : EntitySpawnManager
    {
        public enum PointSpawnerState
        {
            Stopped,
            StartDelay,
            WaveDelay,
            Spawning,
            SpawnWait,
            MaxCapWait
        }

        [Header("Point Shop Variables")] [SerializeField]
        private int maxPoints = 3;

        [SerializeField] private int incrementPoint = 1;

        [Header("Spawn Settings")] [SerializeField]
        private float startDelay = 1f;

        [SerializeField] private float spawnInterval = 1f;
        [SerializeField] private float waveInterval = 5f;
        [SerializeField] private float waveIntervalIncrement = 0.5f;
        [SerializeField] private float waitInterval = 3f;
        [SerializeField] private float waitIntervalIncrement = 1f;

        [Space(15)] [SerializeField] private float checkRadius = 0.45f;

        [SerializeField] private PointSpawnerState spawnerState;

        public SpawnItem[] spawnItems;

        private float _spawnInterval;
        private float _waitInterval;

        private GameObject[] entitiesToSpawn;

        //Entity and their cost
        private readonly Dictionary<GameObject, int> entityMenu = new();

        //Copy of all entity gameobjects
        private readonly List<GameObject> possibleEntities = new();
        private int toSpawnIndex;

        public float CurrentWaveInterval { get; private set; }

        public int TotalWaves { get; private set; }

        private void Start()
        {
            possibleEntities.Clear();
            entityMenu.Clear();
            for (var i = 0; i < spawnItems.Length; i++)
            {
                possibleEntities.Add(spawnItems[i].item);
                entityMenu.Add(spawnItems[i].item, spawnItems[i].cost);
                spawnItems[i].InitializeCD();
            }

            SwitchState(PointSpawnerState.StartDelay);
        }

        private void Update()
        {
            UpdateState(spawnerState);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position, Vector3.one);
        }

        public event Action onWaveStarted;
        public event Action onWaveStarting;

        //Runs once when switching state
        private void EnterState(PointSpawnerState state)
        {
            switch (state)
            {
                case PointSpawnerState.Stopped:
                    break;
                case PointSpawnerState.StartDelay:
                    CurrentWaveInterval = startDelay;
                    break;
                case PointSpawnerState.WaveDelay:
                    onWaveStarting?.Invoke();
                    CurrentWaveInterval = waveInterval;
                    waveInterval += waveIntervalIncrement;
                    toSpawnIndex = 0;

                    entitiesToSpawn = GetListOfEntities();
                    break;
                case PointSpawnerState.Spawning:
                    TotalWaves++;
                    for (var i = 0; i < spawnItems.Length; i++) spawnItems[i].IncrementCD();
                    break;
                case PointSpawnerState.SpawnWait:
                    _waitInterval = waitInterval;
                    break;
                case PointSpawnerState.MaxCapWait:
                    break;
            }

            spawnerState = state;
        }

        //Runs on update
        private void UpdateState(PointSpawnerState state)
        {
            switch (state)
            {
                case PointSpawnerState.Stopped:
                    break;
                case PointSpawnerState.StartDelay:
                    if (CurrentWaveInterval <= 0f)
                    {
                        SwitchState(PointSpawnerState.WaveDelay);
                        CurrentWaveInterval = waveInterval;
                    }
                    else
                    {
                        CurrentWaveInterval -= Time.deltaTime;
                    }

                    break;
                case PointSpawnerState.WaveDelay:
                    if (CurrentWaveInterval <= 0f)
                    {
                        SwitchState(PointSpawnerState.Spawning);
                        CurrentWaveInterval = waveInterval;
                    }
                    else
                    {
                        CurrentWaveInterval -= Time.deltaTime;
                    }

                    break;
                case PointSpawnerState.Spawning:
                    if (_spawnInterval <= 0f)
                    {
                        var entity = SpawnEntity(entitiesToSpawn[toSpawnIndex]);
                        toSpawnIndex++;
                        _spawnInterval = spawnInterval;

                        if (toSpawnIndex >= entitiesToSpawn.Length)
                        {
                            SwitchState(PointSpawnerState.SpawnWait);
                            break;
                        }
                    }
                    else
                    {
                        _spawnInterval -= Time.deltaTime;
                    }

                    if (spawnParent.childCount >= maxAlive) SwitchState(PointSpawnerState.MaxCapWait);
                    break;
                case PointSpawnerState.SpawnWait:

                    if (_waitInterval <= 0f)
                    {
                        SwitchState(PointSpawnerState.WaveDelay);
                    }
                    else
                    {
                        //Check if map is empty, do early spawn
                        if (spawnParent.childCount == 0) _waitInterval = 0f;

                        _waitInterval -= Time.deltaTime;
                    }

                    break;
                case PointSpawnerState.MaxCapWait:
                    if (spawnParent.childCount < maxAlive) SwitchState(PointSpawnerState.Spawning);
                    break;
            }
        }

        //Runs once when exiting the state
        private void ExitState(PointSpawnerState state)
        {
            switch (state)
            {
                case PointSpawnerState.Stopped:
                    break;
                case PointSpawnerState.StartDelay:
                    break;
                case PointSpawnerState.WaveDelay:
                    onWaveStarted?.Invoke();
                    break;
                case PointSpawnerState.Spawning:
                    maxPoints += incrementPoint;
                    break;
                case PointSpawnerState.SpawnWait:
                    waitInterval += waitIntervalIncrement;
                    break;
                case PointSpawnerState.MaxCapWait:
                    break;
            }
        }

        //Used to transition between the states
        private void SwitchState(PointSpawnerState state)
        {
            ExitState(spawnerState);
            spawnerState = state;
            EnterState(spawnerState);
        }

        private GameObject[] GetListOfEntities()
        {
            var attempts = 100;
            var pointsToSpend = maxPoints;
            var potentialEntites = new List<GameObject>(possibleEntities);
            var confirmedEntities = new List<GameObject>();

            while (pointsToSpend > 0 && attempts > 0)
            {
                var selectedEntity = MathHelper.RandomFromArray(potentialEntites.ToArray(), out var index);

                if (CanSelectEntity(index))
                {
                    if (entityMenu[selectedEntity] <= pointsToSpend)
                    {
                        pointsToSpend -= entityMenu[selectedEntity];
                        confirmedEntities.Add(selectedEntity);
                    }
                    else
                    {
                        //Cut off those that are no longer in the price range
                        potentialEntites.RemoveRange(index, potentialEntites.Count - index);
                    }
                }
                else
                {
                    potentialEntites.RemoveAt(index);
                }

                attempts--;
            }

            return confirmedEntities.ToArray();
        }

        private bool CanSelectEntity(int index)
        {
            if (spawnItems[index].spawnAfterWave <= TotalWaves)
                if (spawnItems[index].isWaveCDDone)
                {
                    spawnItems[index].ResetWaveCD();
                    return true;
                }

            return false;
        }

        private GameObject SpawnEntity(GameObject entity)
        {
            var attempts = 50;
            while (attempts > 0)
            {
                var spawnLoc = transform.position + MathHelper.InArea(new Vector3(spawnArea.x, 0f, spawnArea.y));
                if (CheckSpawnValid(spawnLoc, obstacleLayer, checkRadius)) return Spawn(entity, spawnLoc);

                attempts--;
            }

            Debug.Log("Could not find a place to spawn");
            SwitchState(PointSpawnerState.Stopped);
            return null;
        }

        [Serializable]
        public class SpawnItem
        {
            public GameObject item;
            public int cost;
            public int spawnAfterWave;

            [Tooltip("Determines how many waves between this entity's spawns. 0 = every wave, 1 = alternate...")]
            public int waveCooldown;

            private int _waveCD;

            public bool isWaveCDDone => _waveCD >= waveCooldown;

            public void IncrementCD()
            {
                _waveCD++;
            }

            public void ResetWaveCD()
            {
                _waveCD = 0;
            }

            public void InitializeCD()
            {
                _waveCD = waveCooldown;
            }
        }
    }
}