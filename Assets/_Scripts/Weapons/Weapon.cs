using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace WeaponNamespace
{
    public class Weapon : MonoBehaviour
    {
        [Tooltip("Unique ID for every weapon. Used when finding a weapon in the weapon catalogue")]
        [SerializeField] private int weaponID;
        [SerializeField] private Transform shootAnchor;
        
        public float damage;
        public float shootInterval;
        public GameObject projectile;

        private float _shootInterval;

        public int WeaponID => weaponID;

        private void Start()
        {
            _shootInterval = shootInterval;
        }

        public virtual void Update()
        {
            TickShootInterval();
        }

        public virtual void Shoot()
        {
            if (_shootInterval < 0)
            {
                GameObject spawnedProjectile = PoolManager.instance.PullFromPool(projectile, obj =>
                {
                    obj.transform.SetPositionAndRotation(shootAnchor.position, shootAnchor.rotation);
                });

                _shootInterval = shootInterval;
            }
        }

        public void TickShootInterval()
        {
            if (_shootInterval > 0)
            {
                _shootInterval -= Time.deltaTime;
            }
        }
    }
}