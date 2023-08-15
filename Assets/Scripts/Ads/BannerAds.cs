using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    public static BannerAds Instance { get; private set; }

    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;
    [SerializeField] string _androidAdUnitId = "Banner_Android";
    string _adUnitId = null; // This will remain null for unsupported platforms.

    void Start()
    {
        // Get the Ad Unit ID for the current platform:
        #if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
        #elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
        #endif

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        // Set the banner position:
        Advertisement.Banner.SetPosition(_bannerPosition);
    }

    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(_adUnitId, options);
    }

    void OnBannerLoaded()
    {
        UnityEngine.Debug.Log("Banner loaded");
        ShowBannerAd();
    }

    void OnBannerError(string message)
    {
        UnityEngine.Debug.Log($"Banner Error: {message}");
    }

    void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        Advertisement.Banner.Show(_adUnitId, options);
    }

    void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }

}
