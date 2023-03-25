using CapybaraAdventure.Player;

namespace CapybaraAdventure.UI
{
    public class ScorePresenter
    {
        private readonly Score _score;
        private readonly ScoreText _scoreText;

        public ScorePresenter(Score score, ScoreText scoreText)
        {
            _score = score;
            _scoreText = scoreText;
        }

        public void Enable()
        {
            _score.OnScoreChanged += UpdateScoreText;
        }

        public void Disable()
        {
            _score.OnScoreChanged -= UpdateScoreText;
        }

        private void UpdateScoreText(int count) => _scoreText.UpdateText(count);
    }
}