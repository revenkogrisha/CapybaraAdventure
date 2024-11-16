using System.Collections.Generic;
using CapybaraAdventure.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace CapybaraAdventure.UI
{
    public class RewardBlock : MonoBehaviour
    {
        [Header("Localization")]
        [SerializeField] private LocalizedString _collectedAlias;
        [SerializeField] private LocalizedString _rewardAlias;
        [SerializeField] private LocalizedString _defeatedAlias;
        
        [Header("Block")]
        [SerializeField] private LocalizeStringEvent _localizedRewardTypeTMP;
        [SerializeField] private TMP_Text _amountTMP;
        [SerializeField] private Image _resourceImage;
        
        [Header("Settings")]
        [SerializeField] private Sprite _coinSprite;
        [SerializeField] private Sprite _foodSprite;
        
        [Header("Settings")]
        [SerializeField] private Color _neutralColor;
        [SerializeField] private Color _positiveColor;

        private readonly Dictionary<LevelPlaythrough.RewardType, LocalizedString> _rewardTypeAliases = new();
        private readonly Dictionary<LevelPlaythrough.ResourceType, Sprite> _resourceTypeSprites = new();

        private void Awake()
        {
            _rewardTypeAliases[LevelPlaythrough.RewardType.Collected] = _collectedAlias;
            _rewardTypeAliases[LevelPlaythrough.RewardType.Reward] = _rewardAlias;
            _rewardTypeAliases[LevelPlaythrough.RewardType.Defeated] = _defeatedAlias;
            
            _resourceTypeSprites[LevelPlaythrough.ResourceType.Coins] = _coinSprite;
            _resourceTypeSprites[LevelPlaythrough.ResourceType.Food] = _foodSprite;
        }

        public void Initialize(LevelPlaythrough.Reward reward)
        {
            _localizedRewardTypeTMP.StringReference = _rewardTypeAliases[reward.Type];
            _localizedRewardTypeTMP.StringReference.RefreshString();
            
            _amountTMP.SetText(reward.Amount.ToString());
            _resourceImage.sprite = _resourceTypeSprites[reward.Resource];

            if (reward.Amount <= 0)
            {
                _amountTMP.color = _neutralColor;
                return;
            }
            
            _amountTMP.color = _positiveColor;
        }
    }
}