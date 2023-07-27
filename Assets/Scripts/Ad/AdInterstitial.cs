using UnityEngine;
using UnityEngine.Advertisements;

namespace CapybaraAdventure.Ad
{
    public class AdInterstitial : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string AndroidAdID = "Interstitial_Android";
        private const string IOSAdID = "Interstitial_iOS";

        [SerializeField, Range(0f, 100f)] private float _showChance = 30f;

        private AdTimer _adTimer = AdTimer.Instance;
        private string _adID;

        private void Awake()
        {
            _adID = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSAdID : AndroidAdID;

            Advertisement.Load(_adID, this);
        }

        public void TryShowWithChance()
        {
            if (_adTimer.CanShowAd == false)
            {
                print("Ad blocked by timer!");
                return;
            }

            int randomChance = Random.Range(0, 101);
            if (randomChance <= _showChance)
                Show();
        }

        public void Show()
        {
            Advertisement.Show(_adID, this);
        }

        #region Callbacks
        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("Ad's loaded" + placementId);
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Ad's load error: {error.ToString()} - {message}");
            Advertisement.Load(_adID, this);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Ad's show error: {error.ToString()} - {message}");
            Advertisement.Load(_adID, this);
        }

        public void OnUnityAdsShowStart(string placementId) {  }

        public void OnUnityAdsShowClick(string placementId) {  }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Advertisement.Load(_adID, this);
            _adTimer.BlockAdForPeriod();
        }
        #endregion
    }
}