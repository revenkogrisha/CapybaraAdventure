using Zenject;
using CapybaraAdventure.Other;
using UnityEngine;
using UnityTools.Buttons;
using CapybaraAdventure.Game;
using CapybaraAdventure.Ad;

namespace CapybaraAdventure.UI
{
    public class GameUI : UIBase
    {
        [SerializeField] private UIButton _pauseButton;
        [SerializeField] private UIButton _giveSwordButton;
        [SerializeField] private AdRewarded _swordAdRewarded;

        private PauseManager _pauseManager;
        private PauseScreenProvider _pauseScreenProvider;

        public AdRewarded SwordAdRewarded =>_swordAdRewarded;

        #region MonoBehaviour

        private void OnEnable()
        {
            _pauseButton.OnClicked += PauseGame;
            _giveSwordButton.OnClicked += DeactivateSwordButton;
        }

        private void OnDisable()
        {
            _pauseButton.OnClicked -= PauseGame;
            _giveSwordButton.OnClicked -= DeactivateSwordButton;
        }

        private void Start()
        {
            TweenGiveSwordButton();
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

            await _pauseScreenProvider.Load();

            Conceal();
        }

        private void TweenGiveSwordButton()
        {
            var tweener = new ScreenTweener();
            tweener.TweenSwordButton(_giveSwordButton.transform);
        }

        private void DeactivateSwordButton()
        {
            _giveSwordButton.gameObject.SetActive(false);
        }
    }
}