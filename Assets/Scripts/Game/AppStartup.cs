using CapybaraAdventure.Other;
using Core.Common.ThirdParty;
using IngameDebugConsole;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Game
{
    public class AppStartup : MonoBehaviour
    {
        [SerializeField] private DebugLogManager _ingameDebugLogManager;
        
        private LoadingScreenProvider _loaderProvider;

        private async void Start()
        {
            // NEW
            if (Debug.isDebugBuild == false)
                Object.Destroy(_ingameDebugLogManager.gameObject);
            
            FirebaseService.Initialize();
            // ––––––––––
            
            Application.targetFrameRate = 60;

            await _loaderProvider.LoadGameAsync();
        }

        [Inject]
        private void Construct(LoadingScreenProvider provider)
        {
            _loaderProvider = provider;
        }
    }
}
