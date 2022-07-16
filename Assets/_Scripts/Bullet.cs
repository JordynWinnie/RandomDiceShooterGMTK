using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    public class Bullet : MonoBehaviour
    {
        [HideInInspector] public float damage;
        [HideInInspector] public float speed = 30f;
        [HideInInspector] public float lifeTime = 1f;
        
        private float _lifetime;
        [SerializeField] private Rigidbody rb;

        private void OnEnable()
        {
            rb.AddForce(transform.forward * speed, ForceMode.Impulse);
            _lifetime = lifeTime;
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
            collision.gameObject.TryGetComponent<IDamageable>(out var damageable);
            damageable?.TakeDamage(damage);
        }

        public virtual void DestroyProjectile()
        {
            rb.velocity = Vector3.zero;
            PoolManager.instance.PushToPool(gameObject);
        }
    }
}