using CapybaraAdventure.Other;
using CapybaraAdventure.Player;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Game
{
    public class AppStartup : MonoBehaviour
    {
        private LoadingScreenProvider _loaderProvider;
        private PlayerData _playerData;

        private async void Start()
        {
            Application.targetFrameRate = 60;

            if (_playerData.IsCutsceneWatched == true)
            {
                await _loaderProvider.LoadGameAsync();
            }
            else
            {
                _playerData.IsCutsceneWatched = true;
                await _loaderProvider.LoadPregameCutsceneAsync();
            }
        }

        [Inject]
        private void Construct(LoadingScreenProvider provider, PlayerData playerData)
        {
            _loaderProvider = provider;
            _playerData = playerData;
        }
    }
}
