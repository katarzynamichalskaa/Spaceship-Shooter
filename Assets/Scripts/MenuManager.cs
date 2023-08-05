using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
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
        SceneManager.LoadScene("Menu");
    }
}
