using Zenject;
using CapybaraAdventure.Other;
using UnityEngine;
using UnityTools.Buttons;
using CapybaraAdventure.Game;

namespace CapybaraAdventure.UI
{
    public class GameUI : UIBase
    {
        private const int SwordAdId = 3;

        [SerializeField] private UIButton _pauseButton;
        [SerializeField] private UIButton _giveSwordButton;

        private PauseManager _pauseManager;
        private PauseScreenProvider _pauseScreenProvider;

        #region MonoBehaviour

        private void OnEnable()
        {
            _pauseButton.OnClicked += PauseGame;
            _giveSwordButton.OnClicked += ActivateSwordAd;
        }

        private void OnDisable()
        {
            _pauseButton.OnClicked -= PauseGame;
            _giveSwordButton.OnClicked -= ActivateSwordAd;
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

        private void ActivateSwordAd()
        {
            _giveSwordButton.gameObject.SetActive(false);
            YGAdsProvider.ShowRewardedAd(SwordAdId);
        }
    }
}