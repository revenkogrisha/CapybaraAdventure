using UnityEngine;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using System.Collections.Generic;

namespace CapybaraAdventure.Ad
{
    public class AppodealInterstitial : MonoBehaviour, IAppodealInitializationListener
    {
        private readonly string appKey = AppodealStartup.AppKey;

        public bool IsLoaded => Appodeal.IsLoaded(AppodealAdType.Interstitial) == true;

        private void Start()
        {
            Appodeal.Initialize(appKey, AppodealAdType.Interstitial, this);
        }

        public void TryShow()
        {
            if (IsLoaded == true)
                Appodeal.Show(AppodealAdType.Interstitial);
        }

        public void OnInitializationFinished(List<string> errors) {  }
    }
}