using CapybaraAdventure.UI;
using Core.Player;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI
{
    public class SkinItemView : TweenClickable, IPointerClickHandler
    {
        [SerializeField] private GameObject _selectedPanel;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _nameTMP;

        private SkinsPanel _panel;

        public SkinName Name { get; private set; }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_panel == null)
            {
                Debug.LogError($"{nameof(SkinItemView)}::{nameof(OnPointerClick)}: root panel is null when clicking on item view!");
                return;
            }

            _panel.CommandItemDisplay(Name);
        }

        public void Initialize(SkinsPanel panel, SkinName skinName, Sprite menuItem)
        {
            _panel = panel;

            Name = skinName;

            _image.sprite = menuItem;
            _nameTMP.SetText(Name.ToString());

            OnPointerUp(null);
        }

        public void SetSelected(bool value) => 
            _selectedPanel.SetActive(value);
    }
}