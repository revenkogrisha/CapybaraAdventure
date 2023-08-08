using CapybaraAdventure.Other;
using CapybaraAdventure.Player;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Game
{
    public class AppStartup : MonoBehaviour
    {
        private LoadingScreenProvider _loaderProvider;

        private async void Start()
        {
            Application.targetFrameRate = 60;
//
            await _loaderProvider.LoadPregameCutsceneAsync();
        }

        [Inject]
        private void Construct(LoadingScreenProvider provider)
        {
            _loaderProvider = provider;
        }
    }
}
