using CapybaraAdventure.Other;
using Core.Common.ThirdParty;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Game
{
    public class AppStartup : MonoBehaviour
    {
        private LoadingScreenProvider _loaderProvider;

        private async void Start()
        {
            // NEW
            FirebaseService.Initialize();
            
            Application.targetFrameRate = 60;

            await _loaderProvider.LoadPregameCutsceneAsync();
        }

        [Inject]
        private void Construct(LoadingScreenProvider provider)
        {
            _loaderProvider = provider;
        }
    }
}
