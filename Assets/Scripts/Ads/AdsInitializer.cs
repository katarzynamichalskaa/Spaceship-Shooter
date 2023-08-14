using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Advertisements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;
    string currentSceneName;
    [SerializeField] RewardedAdsButton rewardedAdsButton = null;
    [SerializeField] BannerAds bannerAds = null;

    void Start()
    {
         currentSceneName = SceneManager.GetActiveScene().name;
    }

    void Awake()
    {
        InitializeAds();
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

        if(rewardedAdsButton != null && currentSceneName == "Game") 
        {
            rewardedAdsButton.LoadAd();
        }
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
