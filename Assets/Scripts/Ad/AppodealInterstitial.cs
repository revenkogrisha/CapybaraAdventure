using UnityEngine;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using System.Collections.Generic;

namespace CapybaraAdventure.Ad
{
    public class AppodealInterstitial : MonoBehaviour, IAppodealInitializationListener
    {
        private readonly string _appKey = AppodealStartup.AppKey;

        [SerializeField] private int _showChance = 40;

        public bool IsLoaded => Appodeal.IsLoaded(AppodealAdType.Interstitial) == true;
        public bool CanShow => Appodeal.CanShow(AppodealAdType.Interstitial) == true;
        public bool IsPrecache => Appodeal.IsPrecache(AppodealAdType.Interstitial) == true;

        private void Start()
        {
            Appodeal.Initialize(_appKey, AppodealAdType.Interstitial, this);
        }

        public void TryShowWithChance()
        {
            int randomChance = Random.Range(0, 101);
            if (randomChance <= _showChance)
                Show();
        }

        public void Show()
        {
            if (IsLoaded == true && CanShow == true && IsPrecache == false)
                Appodeal.Show(AppodealShowStyle.Interstitial);
            else
                Appodeal.Cache(AppodealAdType.Interstitial);
        }

        public void OnInitializationFinished(List<string> errors) {  }
    }
}