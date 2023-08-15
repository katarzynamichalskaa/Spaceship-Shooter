using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Advertisements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    public static AdsInitializer Instance { get; private set; }

    [SerializeField] string _androidGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;
    [SerializeField] RewardedAdsButton rewardedAdsButton = null;
    [SerializeField] BannerAds bannerAds = null;
    bool firstChance = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeAds();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game" && firstChance)
        {
            GameObject canvas = GameObject.Find("Canvas");
            rewardedAdsButton = canvas.GetComponentInChildren<RewardedAdsButton>();

            if (rewardedAdsButton != null)
            {
                rewardedAdsButton.LoadAd();
                firstChance = false;
            }
        }
        
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            firstChance = true;
        }
    }

    public void InitializeAds()
    {
        #if UNITY_IOS
            _gameId = _iOSGameId;
        #elif UNITY_ANDROID
            _gameId = _androidGameId;
        #elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
        #endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }


    public void OnInitializationComplete()
    {
        UnityEngine.Debug.Log("Unity Ads initialization complete.");
        
        if(bannerAds != null) 
        {
            bannerAds.LoadBanner();
        }

    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        UnityEngine.Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

}
