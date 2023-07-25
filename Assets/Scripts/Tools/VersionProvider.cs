using UnityEngine;
using TMPro;

namespace CapybaraAdventure.UI
{
    public class VersionProvider : MonoBehaviour
    {
        private const string Prefix = "v.";

        [SerializeField] private TextMeshProUGUI _text;

        private void Awake()
        {
            string version = Application.version;
            _text.text = $"{Prefix} {version}";
        }
    }
}