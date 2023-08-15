using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Diagnostics;
using System.Collections.Generic;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static RewardedAdsButton Instance { get; private set; }

    [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    string _adUnitId = null;
    HealthManager healthManager;
    string[] UInames = { "RewardedAdsButton", "Rewarded", "Back" };
    public List<GameObject> UI = new List<GameObject>();

    void Start()
    {
        AddToList(UInames, UI);
        _showAdButton = UI[0].GetComponent<Button>();
    }

    void Awake()
    {
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

        _showAdButton.interactable = false;
    }

    public void LoadAd()
    {
        UnityEngine.Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        UnityEngine.Debug.Log("Ad Loaded: " + adUnitId);


        if (adUnitId.Equals(_adUnitId))
        {
            _showAdButton.onClick.AddListener(ShowAd);
            _showAdButton.interactable = true;
        }

    }

    public void ShowAd()
    {
        _showAdButton.interactable = false;
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        healthManager = GameObject.Find("Player").GetComponent<HealthManager>();

        if (healthManager != null)
        {
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                healthManager.PlayerReward();
                OnDestroy();
            }
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        UnityEngine.Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        UnityEngine.Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        _showAdButton.onClick.RemoveAllListeners();
    }

    public void SetActive( bool active)
    {
        foreach (GameObject gameObject in UI)
        {
            gameObject.SetActive(active);
        }
    }

    void AddToList(string[] table, List<GameObject> list)
    {
        foreach (string obj in table)
        {
            GameObject gm = GameObject.Find(obj);
            list.Add(gm);
        }

    }
}
