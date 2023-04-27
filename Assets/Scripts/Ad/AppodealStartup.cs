using UnityEngine;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;

namespace CapybaraAdventure.Ad
{
    public class AppodealStartup : MonoBehaviour
    {
        public const string AppKey = "ca-app-pub-1842740529238552~7359360435";

        private void Start() 
        { 
            int adTypes = AppodealAdType.Interstitial 
            | AppodealAdType.RewardedVideo; 

            string appKey = AppKey;

            AppodealCallbacks.Sdk.OnInitialized += OnInitializationFinished;

            Appodeal.Initialize(appKey, adTypes);
        } 
        
        #region Initialization Callback

        public void OnInitializationFinished(
            object sender,
            SdkInitializedEventArgs args)
        {
            Debug.Log("Initialization Finished");

            AppodealCallbacks.Sdk.OnInitialized -= OnInitializationFinished;
        }

        #endregion
    }
}
