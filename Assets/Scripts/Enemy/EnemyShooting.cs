using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Shooting
{
    float spawnInterval = 1f;
    float timer = 0f;

    void Start()
    {
        wingsNames = null;
        weaponNames = null;
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            PrepareBullets(30f, bulletPrefab, 1, false);
            timer = 0f;
        }
    }
}
