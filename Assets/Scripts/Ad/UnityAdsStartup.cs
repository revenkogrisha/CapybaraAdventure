using UnityEngine;
using UnityEngine.Advertisements;

namespace CapybaraAdventure.Ad
{
    public class UnityAdsStartup : MonoBehaviour, IUnityAdsInitializationListener
    {
        private const string AndroidGameID = "5253776";
        private const string IOSGameID = "5253777";

        private bool _testMode = false;
        private string gameID;

        private void Awake()
        {
            #if UNITY_EDITOR
            _testMode = true;
            #endif

            gameID = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSGameID : AndroidGameID;

            Advertisement.Initialize(gameID, _testMode, this);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Initialized successfully!");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Initialization Error: {error.ToString()} - {message}");
        }
    }
}