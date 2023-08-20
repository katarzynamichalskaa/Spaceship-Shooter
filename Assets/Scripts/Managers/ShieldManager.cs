using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    GameObject player;
    MusicManager musicManager;

    [SerializeField] List<GameObject> shields = new List<GameObject>();

    float spawnInterval = 5f;
    float timer = 0f;

    float minX = -1.2f;
    float maxX = 1.2f;

    void Start()
    {
        player = GameObject.Find("Player");
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
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
        int randomIndex = UnityEngine.Random.Range(0, shields.Count);

        //spawn position
        float randomX = UnityEngine.Random.Range(minX, maxX);
        Vector3 spawnPosition = player.transform.position + Vector3.up * 10f + new Vector3(randomX, 0f, 0f);

        GameObject shieldPrefab = shields[randomIndex];

        if (randomIndex == 0)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, 180f);
            Instantiate(shieldPrefab, spawnPosition, rotation);
        }
        else
        {
            Instantiate(shieldPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
