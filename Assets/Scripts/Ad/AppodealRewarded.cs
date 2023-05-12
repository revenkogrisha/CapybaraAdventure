using System;
using System.Collections;
using System.Collections.Generic;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using UnityEngine;
using UnityTools.Buttons;

namespace CapybaraAdventure.Ad
{
    public class AppodealRewarded : MonoBehaviour, IAppodealInitializationListener
    {
        public const float CheckForAdInterval = 0.5f;

        private readonly string _appKey = AppodealStartup.AppKey;

        [SerializeField] private UIButton _showButton;

        public bool IsLoaded => Appodeal.IsLoaded(AppodealAdType.RewardedVideo) == true;
        public bool CanShow => Appodeal.CanShow(AppodealAdType.RewardedVideo) == true;
        public bool IsPrecache => Appodeal.IsPrecache(AppodealAdType.RewardedVideo) == true;

        public event Action OnRewardGotten;

        #region MonoBehavior

        private void Awake()
        {
            SwitchButtonStatus(false);

            StartCoroutine(CheckForAd());
        }

        private void OnEnable()
        {
            _showButton.OnClicked += Show;

            AppodealCallbacks.RewardedVideo.OnFinished += OnRewardedVideoFinished;
        }

        private void OnDisable()
        {
            _showButton.OnClicked -= Show;

            AppodealCallbacks.RewardedVideo.OnFinished -= OnRewardedVideoFinished;
        }

        #endregion

        private void Start()
        {
            Appodeal.Initialize(_appKey, AppodealAdType.RewardedVideo, this);
        }

        public void Show()
        {
            if (IsLoaded == true && CanShow == true && IsPrecache == false)
                Appodeal.Show(AppodealShowStyle.RewardedVideo);
            else
                Appodeal.Cache(AppodealAdType.RewardedVideo);

            SwitchButtonStatus(false);
        }

        private IEnumerator CheckForAd()
        {
            if (IsLoaded == false || CanShow == false || IsPrecache == true)
            {
                Appodeal.Cache(AppodealAdType.RewardedVideo);
                yield return new WaitForSeconds(CheckForAdInterval);
            }

            SwitchButtonStatus(true);
        }

        private void SwitchButtonStatus(bool value) => _showButton.OriginalButton.interactable = value;

        #region Callbacks

        private void OnRewardedVideoFinished(object sender, RewardedVideoFinishedEventArgs args)
        {
            OnRewardGotten?.Invoke();
        }

        public void OnInitializationFinished(List<string> errors) {  }

        #endregion
    }
}