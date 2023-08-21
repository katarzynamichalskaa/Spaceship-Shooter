using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] GameObject autocanonsBulletPrefab;
    [SerializeField] GameObject zapperBulletPrefab;
    HealthManager healthManager;
    MusicManager musicManager;
    float lastShootTime;
    float shootInterval = 0.5f;
    float bulletSpeed = 30f;
    bool changed = true;
    protected string[] wingsNames = { "LeftWing", "RightWing", "RocketLeftWing", "RocketRightWing", "RocketLeftWingWing", "RocketRightWingWing" };
    protected string[] weaponNames = { "Rockets", "AutoCanons", "Zapper" };

    [SerializeField] List<GameObject> weapons = new List<GameObject>();
    [SerializeField] List<Transform> bulletSpawn = new List<Transform>();
    [SerializeField] List<Animator> animators = new List<Animator>();

    public enum EquippedWeapon { None, Rockets, Autocanons, Zapper }

    private EquippedWeapon currentWeapon = EquippedWeapon.None;

    void Start()
    {
        AddToList(wingsNames, weaponNames);

        SetActive(weapons, false);

        healthManager = GameObject.Find("Player").GetComponent<HealthManager>();
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();

        changed = true;

    }

    void Update()
    {
        if(HealthManager.ReturnCurrentHealth() <= 0)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;

            if (Input.GetMouseButtonDown(0) && Time.time > lastShootTime + shootInterval)
            {
                Shoot();
                lastShootTime = Time.time;
            }

            if (Time.time > lastShootTime + shootInterval)
            {
                DeactivateAnimations();
            }

            if (ShopManager.rocketsEquiped && changed)
            {
                SetActive(weapons, false, weapons[0]);
                currentWeapon = EquippedWeapon.Rockets;
                changed = false;
            }
            if (ShopManager.autocanonsEquiped && changed)
            {
                SetActive(weapons, false, weapons[1]);
                currentWeapon = EquippedWeapon.Autocanons;
                changed = false;
            }
            if (ShopManager.zapperEquiped && changed)
            {
                SetActive(weapons, false, weapons[2]);
                currentWeapon = EquippedWeapon.Zapper;
                changed = false;
            }
        }

    }

    void Shoot()
    {
        musicManager.PlaySoundEffect("Laser1");

        if (currentWeapon == EquippedWeapon.None)
        {
            PrepareBullets(bulletSpeed, bulletPrefab, 1);
        }

        if (currentWeapon == EquippedWeapon.Rockets)
        {
            shootInterval = 0.75f;
            PrepareBullets(bulletSpeed, bulletPrefab, bulletSpawn.Count);
            animators[0].enabled = true;
            animators[0].speed = 2.5f;

        }
        if (currentWeapon == EquippedWeapon.Autocanons)
        {
            shootInterval = 0.4f;
            PrepareBullets(bulletSpeed, autocanonsBulletPrefab, 1);
            animators[1].enabled = true;
            animators[1].speed = 2f;

        }
        if (currentWeapon == EquippedWeapon.Zapper)
        {
            shootInterval = 0.3f;
            PrepareBullets(bulletSpeed, zapperBulletPrefab, 1);
            animators[2].enabled = true;
            animators[2].speed = 3f;

        }


    }

    protected void PrepareBullets(float bulletSpeed, GameObject bulletPrefab, int stopIterating, bool isPlayer = true)
    {
        for (int i = 0; i < bulletSpawn.Count && i <= stopIterating; i++)
        {
            StartCoroutine(CreateBulletWithDelay(bulletSpeed, bulletPrefab, bulletSpawn[i].transform, i, isPlayer));
        }
    }


    public void SetActive(List<GameObject> list, bool active, GameObject gm = null)
    {
        foreach (GameObject gameObject in list)
        {
            gameObject.SetActive(active);
        }

        if (gm != null)
        {
            gm.SetActive(!active);
        }
    }

    void AddToList(string[] wings, string[] weaponsTable)
    {
        foreach (string obj in wings)
        {
            Transform transform = GameObject.Find(obj).GetComponent<Transform>();
            bulletSpawn.Add(transform);
        }
        foreach (string obj in weaponsTable)
        {
            GameObject gm = GameObject.Find(obj);
            animators.Add(gm.GetComponent<Animator>());
            weapons.Add(gm);
        }

    }

    void DeactivateAnimations()
    {
        foreach (GameObject weapon in weapons)
        {
            Animator animator = weapon.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Rebind();
            }
        }
    }

    IEnumerator CreateBulletWithDelay(float bulletSpeed, GameObject bulletPrefab, Transform spawnTransform, int index, bool isPlayer)
    {
        float delay = 0f;
        Vector3 offset = new Vector3(0f, 0f, 0f);

        //zapper
        if (currentWeapon == EquippedWeapon.Zapper)
        {
            offset = new Vector3(0f, 1f, 0f);
        }

        //rockets 
        if(currentWeapon == EquippedWeapon.Rockets && (index == 0 || index == 1))
        {
            delay = 0.15f;
        }

        if (index == 2 || index == 3)
        {
            delay = 0f;
        }
        else if (index == 4 || index == 5)
        {
            delay = 0.3f;
        }

        yield return new WaitForSeconds(delay);

        //any type of weapon
        GameObject bullet = Instantiate(bulletPrefab, spawnTransform.position + offset, spawnTransform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if(isPlayer)
        {
            rb.velocity = spawnTransform.up * bulletSpeed;

        }
        else
        {
            rb.velocity = new Vector3(0f, -bulletSpeed, 0f);
        }
    }
}