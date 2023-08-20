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
    MusicManager musicManager;
    List<GameObject> hearts = new List<GameObject>();
    List<GameObject> shields = new List<GameObject>();
    string[] heartNames = { "Hearth1", "Hearth2", "Hearth3"};
    string[] shieldNames = { "Front Shield Variant", "Round Shield Variant", "Invincibility Shield Variant" };
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

        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        menu = GameObject.Find("Menu").GetComponent<Button>();

        currentHealth = maxHealth;
        AddToList(heartNames, hearts);
        AddToList(shieldNames, shields);
        SetActive(shields, false);
        rewardedAdsButton.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("PlayerBullet")) && !isInvulnerable)
        {
            Destroy(collision.gameObject);
            TakeDamage();
        }
        else if((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("PlayerBullet")) && isInvulnerable)
        {
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("SlowDown"))
        {
            musicManager.PlaySoundEffect("Item1");
            shields[0].SetActive(true);
            Time.timeScale = 0.5f;
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Shield"))
        {
            musicManager.PlaySoundEffect("Item1");
            shields[2].SetActive(true);
            currentHealth = currentHealth + 1;
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("KillTouched"))
        {
            musicManager.PlaySoundEffect("Item1");
            shields[1].SetActive(true);
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage()
    {
        musicManager.PlaySoundEffect("Lose4");
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
        musicManager.PlaySoundEffect("Lose2");
        rewardedAdsButton.SetActive(true);
        Time.timeScale = 0f;
        menu.gameObject.SetActive(false);
    }

    private void ResetInvulnerability()
    {
        isInvulnerable = false;
    }

    void AddToList(string[] names, List<GameObject> list)
    {
        foreach (string obj in names)
        {
            GameObject gm = GameObject.Find(obj);
            list.Add(gm);
        }
    }

    public void SetActive(List<GameObject> list, bool active)
    {
        foreach (GameObject gameObject in list)
        {
            gameObject.SetActive(active);
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
        musicManager.PlaySoundEffect("Life2");

        rewardedAdsButton.SetActive(false);
        menu.gameObject.SetActive(true);
        Time.timeScale = 1f;
        currentHealth = 1;
        UpdateHearts();

        isInvulnerable = true;
        StartCoroutine(ResetInvulnerability(GameObject.Find("Damage").GetComponent<Renderer>().material, new Color(1.0f, 0.2f, 0.0f, 0.8f)));
        Invoke("ResetInvulnerability", invulnerabilityDuration);
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
