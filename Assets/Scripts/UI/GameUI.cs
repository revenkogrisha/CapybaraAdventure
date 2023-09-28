using Zenject;
using CapybaraAdventure.Other;
using UnityEngine;
using UnityEngine.UI;
using UnityTools.Buttons;
using CapybaraAdventure.Game;
using CapybaraAdventure.Ad;
using UnityTools;

namespace CapybaraAdventure.UI
{
    public class GameUI : UIBase
    {
        public const int GiveSwordButtonShowChance = 40;

        [Header("UI Elements")]
        [SerializeField] private UIButton _pauseButton;
        [SerializeField] private UIButton _giveSwordButton;
        [SerializeField] private Slider _questBar;
        [field: SerializeField] public JumpButton JumpButton { get; private set; }
        [field: SerializeField] public JumpSlider JumpSlider { get; private set; }

        [Header("Ads Settings")]
        [SerializeField] private AdRewarded _swordAdRewarded;

        private PauseManager _pauseManager;
        private PauseScreenProvider _pauseScreenProvider;
        private ScreenTweener _tweener;


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
            _tweener = new ScreenTweener();

            _giveSwordButton.transform.localScale = Vector2.zero;
            bool isSucceeded = Tools.GetChance(GiveSwordButtonShowChance);
            if (isSucceeded == true)
                ShowGiveSwordButton();
                
            StartPeriodicallyShowQuestBar();
        }

        #endregion

        public void InitQuestBarValues(float min, float max)
        {
            _questBar.minValue = min;
            _questBar.maxValue = max;
        }

        public void UpdateQuestBar(float value)
        {
            // Different logic in future versions
            if (_questBar == null)
                return;

            _questBar.value = value;

            if (value >= _questBar.maxValue)
                Destroy(_questBar.gameObject);
        }

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

        private void ShowGiveSwordButton()
        {
            _tweener.TweenSwordButton(_giveSwordButton.transform);
        }

        private void DeactivateSwordButton()
        {
            _giveSwordButton.gameObject.SetActive(false);
        }

        private void StartPeriodicallyShowQuestBar()
        {
            _tweener.DisplayQuestBarForPeriod(_questBar.transform);
        }
    }
}