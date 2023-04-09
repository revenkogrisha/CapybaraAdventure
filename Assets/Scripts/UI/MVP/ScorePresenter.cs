using CapybaraAdventure.Player;

namespace CapybaraAdventure.UI
{
    public class ScorePresenter
    {
        private readonly Score _score;
        private readonly UIText _scoreText;

        public ScorePresenter(Score score, UIText scoreText)
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

        private void UpdateScoreText(int count)
        {
            string text = $"{count}";
            _scoreText.UpdateText(text);
        }
    }
}