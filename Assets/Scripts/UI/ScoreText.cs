using UnityEngine;
using TMPro;

namespace CapybaraAdventure.UI
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void UpdateText(int scoreCount)
        {
            _text.text = $"{scoreCount}";
        }
    }
}
