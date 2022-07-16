using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class PoolManager : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public GameObject poolItem;
            public Transform container;
            public int prespawnAmount;

            public Pool(GameObject poolItem, int prespawnAmount, Transform container)
            {
                this.poolItem = poolItem;
                this.container = container;
                this.prespawnAmount = prespawnAmount;
            }
        }

        public static PoolManager instance;
        private void Awake() => instance = this;

        public Transform poolContainer;
        public List<Pool> prePools; //For creating any pools before runtime
        public Dictionary<string, Queue<GameObject>> pools { get; private set; } //All the pools

        private void Start()
        {
            prePools = new List<Pool>();
            pools = new Dictionary<string, Queue<GameObject>>();
            for (int i = 0; i < prePools.Count; i++)
            {
                for (int j = 0; j < prePools[i].prespawnAmount; j++)
                {
                    GameObject poolItem = CreatePoolItem(prePools[i].poolItem);
                    PushToPool(poolItem);
                }
            }
        }

        #region Pulling
        
        //Pulling from the pool of the same disabled gameobjects.
        public GameObject PullFromPool(GameObject objectToSpawn)
        {
            string poolName = objectToSpawn.name;
            GameObject poolItem;
            if (pools.ContainsKey(poolName))
            {
                //Greater than 1 because we never want a queue to be completely empty
                //because we wont know what type of gameobject belongs to that queue
                if (pools[poolName].Count > 1)
                {
                    poolItem = pools[poolName].Dequeue();
                }
                else
                {
                    poolItem = CreatePoolItem(objectToSpawn);
                }

            }
            else
            {
                CreatePool(poolName, objectToSpawn);
                poolItem = CreatePoolItem(objectToSpawn);
            }

            poolItem.SetActive(true);
            return poolItem;
        }

        //Pulling from the pool of the same disabled gameobjects with a method to do stuff before enabling the gameobject again 
        public GameObject PullFromPool(GameObject objectToSpawn, Action<GameObject> beforeSetActive)
        {
            string poolName = objectToSpawn.name;
            GameObject poolItem;
            if (pools.ContainsKey(poolName))
            {
                //Greater than 1 because we never want a queue to be completely empty
                //because we wont know what type of gameobject belongs to that queue
                if (pools[poolName].Count > 1)
                {
                    poolItem = pools[poolName].Dequeue();
                }
                else
                {
                    poolItem = CreatePoolItem(objectToSpawn);
                }

            }
            else
            {
                CreatePool(poolName, objectToSpawn);
                poolItem = CreatePoolItem(objectToSpawn);
            }

            beforeSetActive(poolItem);
            poolItem.SetActive(true);
            return poolItem;
        }
        #endregion

        #region Pushing

        //Pushing to the pool will disable the object and re-parent it
        public void PushToPool(GameObject poolItem)
        {
            string poolName = poolItem.name;
            if (!pools.ContainsKey(poolName))
            {
                CreatePool(poolName, poolItem);
            }

            poolItem.SetActive(false);
            for (int i = 0; i < prePools.Count; i++)
            {
                if (prePools[i].poolItem.name == poolItem.name)
                {
                    poolItem.transform.SetParent(prePools[i].container);
                }
            }

            pools[poolName].Enqueue(poolItem);
        }
        #endregion

        #region Creating
        //This region is for the creation of pools which is essentially just a container and items
        
        public void CreatePool(string poolName, GameObject item)
        {
            Queue<GameObject> newPoolQueue = new Queue<GameObject>();
            pools.Add(poolName, newPoolQueue);
            prePools.Add(new Pool(item, 0, CreatePoolContainer(item.name)));
        }

        private GameObject CreatePoolItem(GameObject item)
        {
            GameObject newItem = Instantiate(item);
            newItem.name = item.name;
            newItem.SetActive(false);
            return newItem;
        }

        private Transform CreatePoolContainer(string itemName = "GameObject")
        {
            Transform container = new GameObject(itemName + "_Pool").transform;
            container.SetParent(poolContainer);
            return container;
        }
        #endregion
    }
}