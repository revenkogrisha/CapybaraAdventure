using UnityEngine;

namespace UnityTools.Buttons
{
    /// <summary>
    /// Activates and disactivates GameObjects.
    /// It's also able to add reference only to one object. (Only open or only close)
    /// </summary>
    public class OpenCloseService : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private UIButton _button;

        [Header("Settings")]
        [Tooltip("If required (Can be null; no exception)")]
        [SerializeField] private GameObject _toOpen;
        [Tooltip("If required (Can be null; no exception)")]
        [SerializeField] private GameObject _toClose;

        #region MonoBehaviour

        private void OnEnable()
        {
            _button.OnClicked += OnClicked;
        }

        private void OnDisable()
        {
            _button.OnClicked -= OnClicked;
        }

        #endregion

        private void OnClicked()
        {
            if (_toOpen != null)
                _toOpen.SetActive(true);

            if (_toClose != null)
                _toClose.SetActive(false);
        }
    }
}