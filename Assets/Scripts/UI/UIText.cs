using UnityEngine;
using TMPro;

namespace CapybaraAdventure.UI
{
    public class UIText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _prefix;
        [SerializeField] private bool _addSpaceAfterPrefix;

        private void OnValidate()
        {
            if (_addSpaceAfterPrefix == true)
                _prefix = $"{_prefix} ";
        }

        public void UpdateText(string text) => _text.text = $"{_prefix}" + text;
    }
}
