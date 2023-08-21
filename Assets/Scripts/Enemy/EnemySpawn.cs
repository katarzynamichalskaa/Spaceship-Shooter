using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    GameObject player;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();

    float spawnInterval = 2f; 
    float timer = 0f;
    float gameTimer = 0f;

    float minX = -1.7f; 
    float maxX = 1.7f; 


    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        gameTimer += Time.deltaTime;
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0f; 
        }

        if(gameTimer > 20f && gameTimer < 30f)
        {
            spawnInterval = 1.5f;
        }

        else if (gameTimer > 30f && gameTimer < 60f)
        {
            spawnInterval = 1f;
        }
        
        else if (gameTimer > 60 && gameTimer < 120f)
        {
            spawnInterval = 0.75f;
        }
        else if(gameTimer > 120f)
        {
            spawnInterval = 0.5f;
        }
    }

    void Spawn()
    {
        //generate index
        int randomIndex = UnityEngine.Random.Range(0, enemies.Count);

        //spawn position
        float randomX = UnityEngine.Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, player.transform.position.y + 10f, 0f);

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
