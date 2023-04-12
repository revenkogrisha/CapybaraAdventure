using UnityEngine;
using UnityTools.Buttons;
using TMPro;

namespace CapybaraAdventure.UI
{
    public class UpgradeBlock : MonoBehaviour
    {
        private const int CostIncrease = 15;

        [SerializeField] private UIButton _button;
        [SerializeField] private TextMeshProUGUI _costText;

        public UIButton Button => _button;
        public int Cost { get; private set; } = 15;

        public void Init(int cost)
        {
            Cost = cost;
            UpdateCostText();
        }

        public void HandleUpgrade()
        {
            Cost += CostIncrease;
            UpdateCostText();
        }

        private void UpdateCostText() => _costText.text = $"{Cost}";
    }
}
