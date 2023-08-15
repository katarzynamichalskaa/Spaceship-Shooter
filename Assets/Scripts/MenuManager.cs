using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    HealthManager healthManager;

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Game")
        {
            healthManager = GameObject.Find("Player").GetComponent<HealthManager>();
        }
    }

    public void QuitFromGame()
    {
        UnityEngine.Application.Quit();
    }

    public void BeginGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void Back()
    {
        if(healthManager != null)
        {
            healthManager.Reset();
        }

        SceneManager.LoadScene("Menu");
    }
}
