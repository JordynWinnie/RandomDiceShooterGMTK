using System;
using UnityEngine;

namespace Game
{
    public enum SpawnSetting
    {
        SpawnCap,
        SpawnInterval,
        WaveInterval
    }

    [Serializable]
    public class EntitySpawnModifiers
    {
        public SpawnSetting spawnSetting;
        public int toComplete = 2;
        public float amount;

        [Tooltip("Check if you want the effects to modify after every spawn instead. Disable to modify after wave.")]
        public bool modifyAfterSpawn;

        public Vector2 clampValues;

        [HideInInspector] public int counter;
    }


    public class EntitySpawnManager : MonoBehaviour
    {
        [Header("Manager Settings")] public Vector2 spawnArea = new(2f, 2f);

        public Transform spawnParent;

        [Tooltip(
            "The spawner will stop spawning when it has reached this limit of entities alive on the field. Set zero to infinite")]
        public int maxAlive;

        public LayerMask obstacleLayer;
        public Component[] addOnSpawn;
        [SerializeField] private EntitySpawnModifiers[] spawnModifiers;
        private GameManager gameManager;

        public virtual void Start()
        {
        }

        protected void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea.x, 0f, spawnArea.y));
        }

        public event Action<GameObject> onSpawn;
        public event Action<int> onReachedEntityCap;

        protected GameObject Spawn(GameObject prefab, Vector3 position)
        {
            var entity = Instantiate(prefab, position, Quaternion.identity, spawnParent);
            entity.transform.forward = -Vector3.forward;

            for (var i = 0; i < addOnSpawn.Length; i++) entity.AddComponent(addOnSpawn[i].GetType());

            onSpawn?.Invoke(entity);
            return entity;
        }

        protected bool CheckSpawnValid(Vector3 position, LayerMask layer, float checkRadius = 0.25f)
        {
            return !Physics.CheckSphere(position, checkRadius, layer);
        }

        protected bool HasReachedEntityCap()
        {
            if (maxAlive > 0)
            {
                if (spawnParent.childCount >= maxAlive) onReachedEntityCap?.Invoke(maxAlive);

                return spawnParent.childCount >= maxAlive;
            }

            return false;
        }
    }
}