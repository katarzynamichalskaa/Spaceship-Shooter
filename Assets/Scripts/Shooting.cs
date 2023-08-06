using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    Transform leftGun;
    Transform rightGun;
    float bulletSpeed = 20f;
    float shootInterval = 0.5f;
    float lastShootTime;

    void Start()
    {
        leftGun  = GameObject.Find("LeftWing").GetComponent<Transform>();
        rightGun = GameObject.Find("RightWing").GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > lastShootTime + shootInterval)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    void Shoot()
    {
        GameObject bulletLeft = Instantiate(bulletPrefab, leftGun.position, leftGun.rotation);
        Rigidbody2D rbLeft = bulletLeft.GetComponent<Rigidbody2D>();
        rbLeft.velocity = leftGun.up * bulletSpeed;

        GameObject bulletRight = Instantiate(bulletPrefab, rightGun.position, rightGun.rotation);
        Rigidbody2D rbRight = bulletRight.GetComponent<Rigidbody2D>();
        rbRight.velocity = rightGun.up * bulletSpeed;
    }
}
