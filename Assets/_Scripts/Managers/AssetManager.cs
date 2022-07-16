using Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class AssetManager : MonoBehaviour
    {
        //Particles
        [SerializeField] private GameObject explosionParticle;

        [SerializeField] private GameObject[] bulletPrefabs;

        public GameObject ExplosionParticle => explosionParticle;
        public GameObject[] BulletPrefabArray => bulletPrefabs;

        public static AssetManager instance;
        public void Awake() => instance = this;

        public GameObject GetBulletPrefab(int bulletID)
        {
            for (int i = 0; i < bulletPrefabs.Length; i++)
            {
                if (bulletPrefabs[i].GetComponent<Bullet>().BulletID == bulletID)
                {
                    return bulletPrefabs[i];
                }
            }

            Debug.LogWarning($"Could not find the bullet prefab with the corresponding ID: {bulletID} in the AssetManager", gameObject);
            return bulletPrefabs[0];
        }
    }
}