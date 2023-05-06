using UnityEngine;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;

namespace CapybaraAdventure.Ad
{
    public class AppodealStartup : MonoBehaviour
    {
        public const string AppKey = "cb70e6b4651b54597e44e198317394a4450217379007e016";
        public const bool TestMode = false;

        private void Awake()
        { 
            int adTypes = AppodealAdType.Interstitial 
            | AppodealAdType.RewardedVideo; 

            string appKey = AppKey;

            AppodealCallbacks.Sdk.OnInitialized += OnInitializationFinished;

            Appodeal.Initialize(appKey, adTypes);
            Appodeal.SetTesting(TestMode);
            //Appodeal.SetLogLevel(AppodealLogLevel.Verbose);
        } 
        
        #region Initialization Callback

        public void OnInitializationFinished(
            object sender,
            SdkInitializedEventArgs args)
        {
            Debug.Log("Initialization Finished");

            var errors = args.Errors;
            if (errors == null)
                print("No errors were occurred");
            else
                print("Errors: " + errors.Count);

            AppodealCallbacks.Sdk.OnInitialized -= OnInitializationFinished;
        }

        #endregion
    }
}
