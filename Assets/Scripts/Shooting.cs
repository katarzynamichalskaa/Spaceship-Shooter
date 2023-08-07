using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] List<GameObject> weapons = new List<GameObject>();
    Transform leftGun;
    Transform rightGun;
    GameObject rocekts;
    GameObject autocanons;
    GameObject zapper;
    Animator animator;
    Animator animator1;
    Animator animator2;
    float bulletSpeed = 20f;
    float shootInterval = 0.5f;
    float lastShootTime;

    void Start()
    {
        rocekts = GameObject.Find("Rockets");
        weapons.Add(rocekts);
        autocanons = GameObject.Find("AutoCanons");
        weapons.Add(autocanons);
        zapper = GameObject.Find("Zapper");
        weapons.Add(zapper);

        SetActive(weapons, false);

        leftGun = GameObject.Find("LeftWing").GetComponent<Transform>();
        rightGun = GameObject.Find("RightWing").GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > lastShootTime + shootInterval)
        {
            Shoot();
            lastShootTime = Time.time;
        }

        if (ShopManager.rocketsBought)
        {
            ChangeWeapon(animator, rocekts);
            SetActive(weapons, false, rocekts);
            ShopManager.rocketsBought = false;
        }
        else if(ShopManager.autocanonsBought)
        {
            ChangeWeapon(animator1, autocanons);
            SetActive(weapons, false, autocanons);

            ShopManager.autocanonsBought = false;
        }
        else if(ShopManager.zapperBought)
        {
            ChangeWeapon(animator2, zapper);
            SetActive(weapons, false, zapper);

            ShopManager.zapperBought = false;
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

    public void ChangeWeapon(Animator animator, GameObject weapon)
    {
        animator = weapon.GetComponent<Animator>();
        animator.enabled = true;
        weapon.SetActive(true);
    }

    public void SetActive(List<GameObject> list, bool active, GameObject gm = null)
    {
        foreach (GameObject gameObject in list)
        {
            gameObject.SetActive(active);
        }

        if(gm != null)
        {
            gm.SetActive(true);
        }
    }
}
