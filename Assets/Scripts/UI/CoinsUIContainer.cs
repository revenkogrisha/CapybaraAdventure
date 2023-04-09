using UnityEngine;

namespace CapybaraAdventure.UI
{
    public class CoinsUIContainer : MonoBehaviour
    {
        [SerializeField] private UIText _text;

        public UIText Text => _text;
    }
}
