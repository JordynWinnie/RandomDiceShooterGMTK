using Core;
using Projectiles;
using UnityEngine;

namespace WeaponNamespace
{
    public class Weapon : MonoBehaviour
    {
        [Tooltip("Unique ID for every weapon. Used when finding a weapon in the weapon catalogue")] [SerializeField]
        private int weaponID;

        [SerializeField] private Transform shootAnchor;

        [Header("Weapon")] [SerializeField] private LayerMask enemyLayer;

        public float shootInterval;
        public GameObject bullet;

        [Header("Projectile")] public float damage = 1f;

        public float lifetime = 1f;
        public float speed = 30f;

        [Tooltip("Increasing this beyond zero will cause the bullet to do explosions.")]
        public float explosionRange;

        [Range(0f, 100f)] public float explosionFalloffPercentage;

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
                GameManager.instance.GunShotSound();
                PoolManager.instance.PullFromPool(bullet, projectile =>
                {
                    projectile.transform.SetPositionAndRotation(shootAnchor.position, shootAnchor.rotation);
                    var bullet = projectile.GetComponent<Bullet>();
                    bullet.damage = damage;
                    bullet.speed = speed;
                    bullet.lifeTime = lifetime;
                    bullet.explosionRange = explosionRange;
                    bullet.explosionFalloffPercentage = explosionFalloffPercentage;
                    bullet.enemyLayer = enemyLayer;
                });

                _shootInterval = shootInterval;
            }
        }

        public void TickShootInterval()
        {
            if (_shootInterval > 0) _shootInterval -= Time.deltaTime;
        }
    }
}