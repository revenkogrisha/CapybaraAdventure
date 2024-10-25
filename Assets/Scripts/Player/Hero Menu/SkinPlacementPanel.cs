using System;
using CapybaraAdventure.UI;
using Core.Player;
using TMPro;
using UnityEngine;

namespace Core.UI
{
    public class SkinPlacementPanel : MonoBehaviour
    {
        [Header("Hero Placement")]
        [SerializeField] private HeroSkinSetter _placementSkinSetter;
        
        [Header("Buttons")]
        [SerializeField] private UIButton _buyButton;
        [SerializeField] private UIButton _selectButton;

        [Header("Cost")]
        [SerializeField] private TMP_Text _costTMP;

        [Header("Colors")]
        [SerializeField] private Color _costAvailableColor = Color.white;
        [SerializeField] private Color _costLockedColor = Color.red;

        public event Action BuyButtonCommand;
        public event Action SelectButtonCommand;

        #region MonoBehaviour

        private void OnEnable()
        {
            _buyButton.OnClicked += InvokeBuyButtonEvent;
            _selectButton.OnClicked += InvokeSelectButtonEvent;
        }

        private void OnDisable()
        {
            _buyButton.OnClicked -= InvokeBuyButtonEvent;
            _selectButton.OnClicked -= InvokeSelectButtonEvent;
        }

        #endregion

        public void SetBuyState(int cost, bool canBuy)
        {
            HideSelectButton();
            _buyButton.SetActive(true);

            _buyButton.Interactable = canBuy;

            if (canBuy == true)
                _costTMP.color = _costAvailableColor;
            else
                _costTMP.color = _costLockedColor;

            _costTMP.text = cost.ToString();
        }

        public void SetSelectableState()
        {
            HideBuyButton();
            ShowSelectButton();
        }

        public void SetSelectedState()
        {
            HideSelectButton();
            HideBuyButton();
        }

        public void DisplayPreset(SkinPreset preset) => 
            _placementSkinSetter.Set(preset);

        public void SetAvailability(bool state)
        {
            _buyButton.Interactable = state;
            if (state == true)
                _costTMP.color = _costAvailableColor;
            else
                _costTMP.color = _costLockedColor;
        }

        private void InvokeBuyButtonEvent()
        {
            BuyButtonCommand?.Invoke();
        }

        private void InvokeSelectButtonEvent() =>
            SelectButtonCommand?.Invoke();

        private void ShowSelectButton()
        {
            _selectButton.SetActive(true);
            _selectButton.Interactable = true;
        }
        
        private void HideBuyButton()
        {
            _buyButton.Interactable = false;
            _buyButton.SetActive(false);
        }

        private void HideSelectButton()
        {
            _selectButton.Interactable = false;
            _selectButton.SetActive(false);
        }
    }
}