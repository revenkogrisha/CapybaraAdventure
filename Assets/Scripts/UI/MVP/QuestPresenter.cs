using CapybaraAdventure.Level;
using System;

namespace CapybaraAdventure.UI
{
    public class QuestPresenter
    {
        private readonly LevelGenerator _levelGenerator;
        private readonly GameUI _gameUI;
        private bool _isQuestBarInitialized = false;

        public QuestPresenter(LevelGenerator levelGenerator, GameUI gameUI)
        {
            _levelGenerator = levelGenerator;
            _gameUI = gameUI;
        }

        public void Enable()
        {
            _levelGenerator.OnHeroXPositionUpdated += OnHeroXPositionUpdatedHandler;

            InitSliderValues();
        }

        public void Disable()
        {
            _levelGenerator.OnHeroXPositionUpdated -= OnHeroXPositionUpdatedHandler;
        }

        private void InitSliderValues()
        {
            float max = _levelGenerator.QuestPlatformXPosition;
            _gameUI.InitQuestBarValues(0f, max);
            _isQuestBarInitialized = true;
        }

        private void OnHeroXPositionUpdatedHandler(float value)
        {
            if (_isQuestBarInitialized == false)
                throw new InvalidOperationException("QuestBar has to be initialized! Use InitSliderValues() before");

            _gameUI.UpdateQuestBar(value);
        }
    }
}
