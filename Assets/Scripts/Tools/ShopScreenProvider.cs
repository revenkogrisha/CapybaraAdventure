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

        [Inject]
        public ShopScreenProvider(
            Canvas canvas,
            IAudioHandler audioHandler,
            PlayerData playerData)
        {
            _canvas = canvas;
            _audioHandler = audioHandler;
            _playerData = playerData;
        }
        
        public async Task<ShopMenuScreen> Load()
        {
            Transform canvasTransform = _canvas.transform;
            ShopMenuScreen screen = await LoadInternal<ShopMenuScreen>(ShopMenu, canvasTransform);

            screen.Init(_playerData);
            screen.InitAudioHandler(_audioHandler);
            screen.gameObject.SetActive(true);
            
            return screen;
        }

        public void Unload() => UnlodadInternalIfCached();
    }
}