using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    MusicManager musicManager;

    void Start()
    {
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            musicManager.PlaySoundEffect("Lose1");
            Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }
}
