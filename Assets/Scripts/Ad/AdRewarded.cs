using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityTools.Buttons;

namespace CapybaraAdventure.Ad
{
    public class AdRewarded : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string AndroidAdID = "Rewarded_Android";
        private const string IOSAdID = "Rewarded_iOS";

        [SerializeField] private UIButton _showButton;

        private string _adID;

        public event Action OnRewardGotten;

        #region MonoBehaviour

        private void Awake()
        {
            _adID = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSAdID : AndroidAdID;

            SwitchButtonStatus(false);

            Advertisement.Load(_adID, this);
        }

        private void OnEnable()
        {
            _showButton.OnClicked += Show;
        }

        private void OnDisable()
        {
            _showButton.OnClicked -= Show;
        }

        #endregion

        public void Show()
        {
            Advertisement.Show(_adID, this);
        }

        private void SwitchButtonStatus(bool value) => _showButton.IsInteractable = value;

        #region Callbacks
        public void OnUnityAdsAdLoaded(string placementId)
        {
            if (placementId.Equals(_adID))
                SwitchButtonStatus(true);
            else
                Advertisement.Load(_adID, this);


            Debug.Log("Ad's loaded: " + placementId);
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Ad's load error: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Ad's show error: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowStart(string placementId) {  }

        public void OnUnityAdsShowClick(string placementId) {  }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                OnRewardGotten?.Invoke();
                Debug.Log("Ad's completed & player's rewarded!");
            }
        }
        #endregion
    }
}