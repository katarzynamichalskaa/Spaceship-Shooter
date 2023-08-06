using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    Transform leftGun;
    Transform rightGun;
    GameObject rocekts;
    Animator animator;
    float bulletSpeed = 20f;
    float shootInterval = 0.5f;
    float lastShootTime;

    void Start()
    {
        rocekts = GameObject.Find("Rockets");
        rocekts.SetActive(false);

        leftGun = GameObject.Find("LeftWing").GetComponent<Transform>();
        rightGun = GameObject.Find("RightWing").GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > lastShootTime + shootInterval)
        {
            Shoot();
            animator.enabled = true;
            lastShootTime = Time.time;
        }

        if(ShopManager.rocketsBought)
        {
            ChangeWeapon();
            ShopManager.rocketsBought = false;
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

        animator.enabled = false;

    }

    public void ChangeWeapon()
    {
        rocekts.SetActive(true);
        animator = rocekts.GetComponent<Animator>();
        animator.enabled = false;
    }
}
