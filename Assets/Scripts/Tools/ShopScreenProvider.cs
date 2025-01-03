using System.Threading.Tasks;
using CapybaraAdventure.Player;
using CapybaraAdventure.UI;
using Core.Audio;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Other
{
    public class ShopScreenProvider : LocalAssetLoader
    {
        public const string ShopMenu = nameof(ShopMenu);
        
        private readonly Canvas _canvas;
        private readonly IAudioHandler _audioHandler;
        private readonly PlayerData _playerData;
        private readonly PurchaseManager _purchaseManager;

        [Inject]
        public ShopScreenProvider(
            Canvas canvas,
            IAudioHandler audioHandler,
            PlayerData playerData,
            PurchaseManager purchaseManager)
        {
            _canvas = canvas;
            _audioHandler = audioHandler;
            _playerData = playerData;
            _purchaseManager = purchaseManager;
        }
        
        public async Task<ShopMenuScreen> Load()
        {
            Transform canvasTransform = _canvas.transform;
            ShopMenuScreen screen = await LoadInternal<ShopMenuScreen>(ShopMenu, canvasTransform);

            screen.Init(_playerData, _purchaseManager);
            screen.InitAudioHandler(_audioHandler);
            screen.gameObject.SetActive(true);
            
            return screen;
        }

        public void Unload() => UnlodadInternalIfCached();
    }
}