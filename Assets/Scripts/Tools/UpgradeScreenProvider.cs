using System.Threading.Tasks;
using UnityEngine;
using CapybaraAdventure.UI;
using CapybaraAdventure.Player;
using CapybaraAdventure.Save;
using Core.Audio;
using Core.Player;
using Zenject;

namespace CapybaraAdventure.Other
{
    public class UpgradeScreenProvider : LocalAssetLoader
    {
        // NEW
        public const string HeroMenu = nameof(HeroMenu);

        private readonly Canvas _canvas;
        private readonly PlayerData _playerData;
        private readonly SaveService _saveService;
        private readonly LoadingScreenProvider _loadingScreenProvider;
        private readonly HeroSkins _heroSkins;
        private readonly IAudioHandler _audioHandler;

        [Inject]
        public UpgradeScreenProvider(
            Canvas canvas,
            PlayerData playerData,
            SaveService saveService,
            LoadingScreenProvider loadingScreenProvider,
            HeroSkins heroSkins,
            IAudioHandler audioHandler)
        {
            _canvas = canvas;
            _playerData = playerData;
            _saveService = saveService;
            _loadingScreenProvider = loadingScreenProvider;
            _heroSkins = heroSkins;
            _audioHandler = audioHandler;
        }

        public async Task<UpgradeScreen> Load()
        {
            Transform canvasTransform = _canvas.transform;
            UpgradeScreen screen = await LoadInternal<UpgradeScreen>(HeroMenu, canvasTransform);

            screen.Init(_playerData, _saveService, _loadingScreenProvider, _heroSkins, _audioHandler);
            screen.InitAudioHandler(_audioHandler);
            screen.gameObject.SetActive(true);
            
            return screen;
        }

        public void Unload() => UnlodadInternalIfCached();
    }
}
