using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int bulletID;

        [HideInInspector] public LayerMask enemyLayer;
        [HideInInspector] public float damage;
        [SerializeField] public float damageMultiplier;
        [HideInInspector] public float speed = 30f;
        [HideInInspector] public float lifeTime = 1f;
        [HideInInspector] public float explosionRange;
        [HideInInspector] public float explosionFalloffPercentage;

        private float _damage;
        private float _lifetime;
        [SerializeField] private Rigidbody rb;

        public int BulletID => bulletID;

        private void OnEnable()
        {
            rb.AddForce(transform.forward * speed, ForceMode.Impulse);
            _lifetime = lifeTime;

            _damage = damage * damageMultiplier;
        }

        private void Update()
        {
            if (_lifetime <= 0f)
            {
                DestroyProjectile();
            }
            else
            {
                _lifetime -= Time.deltaTime;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (explosionRange > 0f)
            {
                
                Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange, enemyLayer);
                foreach (Collider collider in colliders)
                {
                    float distPercentage = (100 - explosionFalloffPercentage) / explosionRange * Vector3.Distance(transform.position, collider.transform.position);
                    float dmgPercentage = _damage / 100 * (100 - distPercentage);
                    DamageComponent(collider.gameObject, dmgPercentage);
                }
                
                GameObject explosionParticle = 
                    Instantiate(AssetManager.instance.ExplosionParticle, transform.position, Quaternion.identity);
                Destroy(explosionParticle, 3f);
                DestroyProjectile();
                return;
            }

            DamageComponent(collision.gameObject, _damage);
        }

        private void DamageComponent(GameObject component, float dmg)
        {
            component.TryGetComponent<IDamageable>(out var damageable);
            damageable?.TakeDamage(dmg);
        }

        public virtual void DestroyProjectile()
        {
            rb.velocity = Vector3.zero;
            PoolManager.instance.PushToPool(gameObject);
        }

        public virtual void CreateExplosion()
        {

        }
    }
}