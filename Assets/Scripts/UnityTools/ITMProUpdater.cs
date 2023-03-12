using TMPro;
using UnityEngine;

namespace UnityTools
{
    public abstract class ITMProUpdater : MonoBehaviour
    {
        protected void UpdateText(TextMeshProUGUI TMPText, string text)
        {
            TMPText.text = text;
        }
    }
}