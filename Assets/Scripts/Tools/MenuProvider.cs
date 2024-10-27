using CapybaraAdventure.Other;
using CapybaraAdventure.Game;
using System.Threading.Tasks;
using UnityEngine;
using CapybaraAdventure.Player;
using Core.Audio;
using Zenject;

namespace CapybaraAdventure.UI
{
    public class MenuProvider : LocalAssetLoader
    {
        //NEW
        public const string MainMenu = nameof(MainMenu);
        
        private readonly Canvas _canvas;
        private readonly GameStartup _gameStartup;
        private UpgradeScreenProvider _upgradeScreenProvider;
        private readonly Score _score;
        private readonly LocalizationManager _localization;
        private readonly IAudioHandler _audioHandler;

        [Inject]
        public MenuProvider(
            Canvas canvas,
            UpgradeScreenProvider upgradeScreenProvider,
            GameStartup gameStartup,
            Score score,
            LocalizationManager localization,
            IAudioHandler audioHandler)
        {
            _canvas = canvas;
            _upgradeScreenProvider = upgradeScreenProvider;
            _gameStartup = gameStartup;
            _score = score;
            _localization = localization;
            _audioHandler = audioHandler;
        }

        public async Task<GameMenu> Load()
        {
            Transform canvasTransform = _canvas.transform;
            GameMenu menu = await LoadInternal<GameMenu>(MainMenu, canvasTransform);

            menu.Init(
                _gameStartup, 
                _upgradeScreenProvider, 
                _score,
                _localization);
            
            menu.InitAudioHandler(_audioHandler);

            return menu;
        }

        public void Unload() => UnlodadInternalIfCached();
    }
}
