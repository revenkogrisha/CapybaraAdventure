using UnityEngine;
using UnityEngine.Advertisements;

namespace CapybaraAdventure.Ad
{
    public class UnityAdsStartup : MonoBehaviour, IUnityAdsInitializationListener
    {
        private const string AndroidGameID = "5061567";
        private const string IOSGameID = "5061566";
        private const bool TestMode = true;

        private string gameID;

        private void Awake()
        {
            gameID = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSGameID : AndroidGameID;

            Advertisement.Initialize(gameID, TestMode, this);
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