using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject Bullet;

    float lastFiredTime = 0f;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] Transform barrelLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lastFiredTime -= Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            if (lastFiredTime < 0)
            {
                var bullet = Instantiate(Bullet, barrelLocation.position, barrelLocation.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * 100f, ForceMode.Impulse);
                lastFiredTime = fireRate;
            }
        }
    }
}
