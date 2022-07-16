using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    public class Bullet : MonoBehaviour
    {
        public float projectileSpeed = 30f;
        public float projectileLifetime = 1f;
        
        [SerializeField] private Rigidbody rb;

        private void Start()
        {
            rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
        }
    }
}