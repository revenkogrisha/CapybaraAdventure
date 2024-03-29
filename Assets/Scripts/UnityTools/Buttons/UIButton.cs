﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityTools.Buttons
{
    public class UIButton : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button _button;

        [Header("Audio")]
        [Tooltip("If required (Can be null; no exception)")]
        [SerializeField] private AudioSource _audio;

        public Button OriginalButton => _button;
        public bool IsLocked { get; private set; }
        public bool IsInteractable
        {
            get => _button.interactable;
            set
            {
                if (IsLocked)
                    return;

                _button.interactable = value;
            }
        }

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

        public void Lock()
        {
            IsInteractable = false;
            IsLocked = true;
        }

        public void Unlock()
        {
            IsLocked = false;
        }

        public void UnlockWithInteraction()
        {
            Unlock();
            IsInteractable = true;
        }

        private void PlaySound()
        {
            if (_audio == null)
                return;

            _audio.Play();
        }

        private void InvokeOnClicked() => OnClicked?.Invoke();
    }
}