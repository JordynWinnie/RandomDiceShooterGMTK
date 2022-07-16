using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class RangedWeapon : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float shootInterval;
        [SerializeField] private GameObject projectile;
        [SerializeField] private Transform shootAnchor;

        private float _shootInterval;

        public virtual void Update()
        {
            TickShootInterval();
        }

        public virtual void Shoot()
        {

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