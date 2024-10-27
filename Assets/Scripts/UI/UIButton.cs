using System;
using Core.Audio;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace CapybaraAdventure.UI
{
    public class UIButton : TweenClickable
    {
        private const float LockShakeDuration = 0.3f;
        private const float LockShakeStrength = 9f;
        private const int LockShakeVibration = 10;
    
        [Header("Settings")]
        [SerializeField] private bool _playSound = true;
    
        [Header("Components")]
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _tmp;

        [Header("Graphics")]
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _disabledSprite;

        [Space]
        [SerializeField] private Color _textNormalColor = Color.white;
        [SerializeField] private Color _textDisabledColor = Color.grey;

        public IAudioHandler AudioHandler { get; set; }

        public TMP_Text TMP => _tmp;

        public bool Interactable
        {
            get => _button.interactable;
            set
            {
                _button.interactable = value;
                _image.sprite = value == true
                    ? _normalSprite
                    : _disabledSprite;

                _tmp.color = value == true
                    ? _textNormalColor
                    : _textDisabledColor;
            }
        }

        public event Action OnClickStarted;
        public event Action OnClicked;
        public event Action OnClickEnded;

        #region MonoBehaviour

        private void OnEnable() => 
            _button.onClick.AddListener(PerformClick);

        private void OnDisable() => 
            _button.onClick.RemoveAllListeners();

        #endregion

        [Inject]
        private void Construct(IAudioHandler audioHandler) =>
            AudioHandler = audioHandler;

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (Interactable == true)
            {
                OnClickStarted?.Invoke();
                base.OnPointerDown(eventData);
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (Interactable == true)
            {
                OnClickEnded?.Invoke();
                base.OnPointerUp(eventData);
            }
            else
            {
                Shake();
            }
        }

        public void Shake() =>
            transform.DOShakePosition(LockShakeDuration, LockShakeStrength, LockShakeVibration);
        
        public void SetActive(bool active) => 
            gameObject.SetActive(active);

        private void PerformClick()
        {
            if (AudioHandler != null && _playSound == true)
                AudioHandler.PlaySound(AudioName.Button, true);
            
            OnClicked?.Invoke();
        }
    }
}