using CapybaraAdventure.Other;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Game
{
    public class AppStartup : MonoBehaviour
    {
        private LoadingScreenProvider _loaderProvider;

        private async void Start()
        {
            await _loaderProvider.LoadSceneAsync();
        }

        [Inject]
        private void Construct(LoadingScreenProvider provider)
        {
            _loaderProvider = provider;
        }
    }
}
