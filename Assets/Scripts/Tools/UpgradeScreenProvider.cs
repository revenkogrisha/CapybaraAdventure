using System.Threading.Tasks;
using UnityEngine;
using CapybaraAdventure.UI;
using CapybaraAdventure.Player;
using CapybaraAdventure.Save;
using Zenject;

namespace CapybaraAdventure.Other
{
    public class UpgradeScreenProvider : LocalAssetLoader
    {
        public const string UpgradeScreen = nameof(UpgradeScreen);

        private readonly Canvas _canvas;
        private readonly PlayerData _playerData;
        private readonly SaveService _saveService;
        private readonly LoadingScreenProvider _loadingScreenProvider;

        [Inject]
        public UpgradeScreenProvider(
            Canvas canvas,
            PlayerData playerData,
            SaveService saveService,
            LoadingScreenProvider loadingScreenProvider)
        {
            _canvas = canvas;
            _playerData = playerData;
            _saveService = saveService;
            _loadingScreenProvider = loadingScreenProvider;
        }

        public async Task<UpgradeScreen> Load()
        {
            Transform canvasTransform = _canvas.transform;
            UpgradeScreen screen = await LoadInternal<UpgradeScreen>(UpgradeScreen, canvasTransform);

            screen.Init(_playerData, _saveService, _loadingScreenProvider);
            return screen;
        }

        public void Unload() => UnlodadInternalIfCached();
    }
}
