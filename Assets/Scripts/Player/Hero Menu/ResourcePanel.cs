using CapybaraAdventure.Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.UI
{
    public class ResourcePanel : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private TMP_Text _coinsTMP;
        [SerializeField] private TMP_Text _foodTMP;
        
        private PlayerData _playerData;

        #region MonoBehaviour

        private void OnEnable()
        {
            DisplayResources();
        }

        #endregion
        
        public void Init(PlayerData playerData) =>
            _playerData = playerData;

        public void DisplayResources()
        {
            _coinsTMP.SetText(_playerData.Coins.ToString());
            _foodTMP.SetText(_playerData.Food.ToString());
        }
    }
}