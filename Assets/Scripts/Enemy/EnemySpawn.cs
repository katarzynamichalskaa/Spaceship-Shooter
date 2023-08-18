using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    GameObject player;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();

    float spawnInterval = 5f; 
    float timer = 0f;

    float minX = -1.7f; 
    float maxX = 1.7f; 


    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0f; 
        }
    }

    void Spawn()
    {
        //generate index
        int randomIndex = UnityEngine.Random.Range(0, enemies.Count);

        //spawn position
        float randomX = UnityEngine.Random.Range(minX, maxX);
        Vector3 spawnPosition = player.transform.position + Vector3.up * 10f + new Vector3(randomX, 0f, 0f);

        GameObject enemyPrefab = enemies[randomIndex];

        if (randomIndex == 0)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, 180f);
            Instantiate(enemyPrefab, spawnPosition, rotation);
        }
        else
        {
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
