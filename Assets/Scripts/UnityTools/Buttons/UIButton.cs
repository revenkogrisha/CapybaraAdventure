using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityTools.Buttons
{
    [RequireComponent(typeof(Button))]
    public sealed class UIButton : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button _button;

        [Header("Audio")]
        [Tooltip("If required (Can be null; no exception)")]
        [SerializeField] private AudioSource _audio;

        public event Action OnClicked;

        #region MonoBehaviour

        private void OnEnable()
        {
            _button.onClick.AddListener(PlaySound);
            _button.onClick.AddListener(InvokeOnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        #endregion

        private void PlaySound()
        {
            if (_audio == null)
                return;

            _audio.Play();
        }

        private void InvokeOnClicked() => OnClicked?.Invoke();
    }
}