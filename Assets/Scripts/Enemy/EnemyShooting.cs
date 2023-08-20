using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Shooting
{
    MusicManager musicManager;
    float spawnInterval = 1.5f;
    float timer = 0f;

    void Start()
    {
        wingsNames = null;
        weaponNames = null;
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            musicManager.PlaySoundEffect("Laser3");
            PrepareBullets(15f, bulletPrefab, 1, false);
            timer = 0f;
        }
    }
}
