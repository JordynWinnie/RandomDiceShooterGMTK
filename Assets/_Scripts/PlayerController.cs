using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;

    private float lastFiredTime = 0f;
    [SerializeField] private float bulletSpeed = 100f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private Transform barrelLocation;

    private void Start()
    {
        
    }

    private void Update()
    {
        lastFiredTime -= Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            if (lastFiredTime < 0)
            {
                var bullet = Instantiate(Bullet, barrelLocation.position, barrelLocation.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * bulletSpeed, ForceMode.Impulse);
                lastFiredTime = fireRate;
            }
        }
    }
}
