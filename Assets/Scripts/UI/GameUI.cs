using Zenject;
using CapybaraAdventure.Other;
using UnityEngine;
using UnityTools.Buttons;
using CapybaraAdventure.Game;

namespace CapybaraAdventure.UI
{
    public class GameUI : UIBase
    {
        [SerializeField] private UIButton _pauseButton;

        private PauseManager _pauseManager;
        private PauseScreenProvider _pauseScreenProvider;

        #region MonoBehaviour

        private void OnEnable()
        {
            _pauseButton.OnClicked += PauseGame;
        }

        private void OnDisable()
        {
            _pauseButton.OnClicked -= PauseGame;
        }

        #endregion

        [Inject]
        private void Construct(
            PauseManager pauseManager,
            PauseScreenProvider pauseScreenProvider)
        {
            _pauseManager = pauseManager;
            _pauseScreenProvider = pauseScreenProvider;
        }

        private async void PauseGame()
        {
            _pauseManager.SetPaused(true);

            var screen = await _pauseScreenProvider.Load();
            screen.Init(_pauseManager, this);

            Conceal();
        }
    }
}