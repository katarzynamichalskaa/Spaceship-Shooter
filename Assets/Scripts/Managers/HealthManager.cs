using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] RewardedAdsButton rewardedAdsButton;
    [SerializeField] Button menu;
    SpriteRenderer spriteRenderer;
    List<GameObject> hearts = new List<GameObject>();
    string[] heartNames = { "Hearth1", "Hearth2", "Hearth3"};
    float invulnerabilityDuration = 1.5f;
    int maxHealth = 3;
    static int currentHealth;
    bool isInvulnerable = false;

    private void Start()
    {
        //find object responsible for demage
        spriteRenderer = GameObject.Find("Damage").GetComponent<SpriteRenderer>();
        //sprite without any damage
        spriteRenderer.sprite = sprites[3];
        menu = GameObject.Find("Menu").GetComponent<Button>();
        currentHealth = maxHealth;
        AddToList(heartNames);
        rewardedAdsButton.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvulnerable)
        {
            Destroy(collision.gameObject);
            TakeDamage();
        }
        else if(collision.gameObject.CompareTag("Enemy") && isInvulnerable)
        {
            Destroy(collision.gameObject);
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
            StartCoroutine(ResetInvulnerability(GameObject.Find("Damage").GetComponent<Renderer>().material, new Color(1.0f, 0.2f, 0.0f, 0.8f)));
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
        rewardedAdsButton.SetActive(true);
        Time.timeScale = 0f;
        menu.gameObject.SetActive(false);
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

    private IEnumerator ResetInvulnerability(Material material, Color blinkColor)
    {
        Color originalColor = material.color;
        float blinkDuration = 0.2f;

        while (isInvulnerable)
        {
            material.color = blinkColor;
            yield return new WaitForSeconds(blinkDuration);
            material.color = originalColor;
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    public void PlayerReward()
    {
        rewardedAdsButton.SetActive(false);
        menu.gameObject.SetActive(true);
        Time.timeScale = 1f;
        currentHealth = 1;
        UpdateHearts();

    }

    public static int ReturnCurrentHealth()
    {
        return currentHealth;
    }

    public void Reset()
    {
        currentHealth = 3;

    }
}
