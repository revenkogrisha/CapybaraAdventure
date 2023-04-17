using UnityEngine;
using UnityTools.Buttons;
using CapybaraAdventure.Other;

namespace CapybaraAdventure.Save
{
    public class ResetProgressService : MonoBehaviour
    {
        [SerializeField] private UIButton _button;

        private SaveService _saveService;
        private LoadingScreenProvider _loadingScreenProvider;

        #region MonoBehaviour

        private void OnEnable()
        {
            _button.OnClicked += ResetProgress;
        }

        private void OnDisable()
        {
            _button.OnClicked -= ResetProgress;
        }

        #endregion

        public void Init(SaveService saveService, LoadingScreenProvider loadingScreenProvider)
        {
            _saveService = saveService;
            _loadingScreenProvider = loadingScreenProvider;
        }

        private async void ResetProgress()
        {
            _saveService.ResetProcess();

            await _loadingScreenProvider.LoadSceneAsync();
        }
    }
}