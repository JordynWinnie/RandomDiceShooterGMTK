using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Projectiles;

namespace WeaponNamespace
{
    public class Weapon : MonoBehaviour
    {
        [Tooltip("Unique ID for every weapon. Used when finding a weapon in the weapon catalogue")]
        [SerializeField] private int weaponID;
        [SerializeField] private Transform shootAnchor;
        
        [Header("Weapon")]
        public float shootInterval;
        public GameObject projectile;
        
        private float _shootInterval;

        [Header("Projectile")]
        public float damage = 1f;
        public float lifetime = 1f;
        public float speed = 30f;

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
                PoolManager.instance.PullFromPool(projectile, projectile =>
                {
                    projectile.transform.SetPositionAndRotation(shootAnchor.position, shootAnchor.rotation);
                    Bullet bullet = projectile.GetComponent<Bullet>();
                    bullet.damage = damage;
                    bullet.speed = speed;
                    bullet.lifeTime = lifetime;
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