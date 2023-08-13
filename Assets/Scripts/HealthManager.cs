using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    SpriteRenderer spriteRenderer;
    List<GameObject> hearts = new List<GameObject>();
    string[] heartNames = { "Hearth1", "Hearth2", "Hearth3"};

    float invulnerabilityDuration = 1.0f;
    int maxHealth = 3;
    int currentHealth; 
    bool isInvulnerable = false; 

    private void Start()
    {
        //find object responsible for demage
        spriteRenderer = GameObject.Find("Damage").GetComponent<SpriteRenderer>();
        //sprite without any damage
        spriteRenderer.sprite = sprites[3];

        currentHealth = maxHealth;
        AddToList(heartNames);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvulnerable)
        {
            Destroy(collision.gameObject);
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        currentHealth--;
        UpdateHearts();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            isInvulnerable = true;
            Invoke("ResetInvulnerability", invulnerabilityDuration);
        }
    }

    private void UpdateHearts()
    {
        spriteRenderer.sprite = sprites[currentHealth];

        for (int i = hearts.Count - 1; i >= 0; i--)
        {
            hearts[i].gameObject.SetActive(i < currentHealth);
        }
    }

    private void Die()
    {
        //open ad
    }

    private void ResetInvulnerability()
    {
        isInvulnerable = false;
    }

    void AddToList(string[] names)
    {
        foreach (string obj in names)
        {
            GameObject gm = GameObject.Find(obj);
            hearts.Add(gm);
        }

    }


}
