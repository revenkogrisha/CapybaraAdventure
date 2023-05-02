using System.Collections.Generic;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using UnityEngine;

namespace CapybaraAdventure.Ad
{
    public class AppodealRewarded : MonoBehaviour, IAppodealInitializationListener
    {
        private readonly string appKey = AppodealStartup.AppKey;

        public bool IsLoaded => Appodeal.IsLoaded(AppodealAdType.RewardedVideo) == true;

        private void Start()
        {
            Appodeal.Initialize(appKey, AppodealAdType.RewardedVideo, this);
        }

        public void Show()
        {
            if (IsLoaded == true)
                Appodeal.Show(AppodealShowStyle.RewardedVideo);
        }

        public void OnInitializationFinished(List<string> errors) {  }
    }
}