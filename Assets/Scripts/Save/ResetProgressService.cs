using UnityEngine;
using UnityTools.Buttons;
using CapybaraAdventure.Other;

namespace CapybaraAdventure.Save
{
    public class ResetProgressService : MonoBehaviour
    {
        [SerializeField] private UIButton _button;

        private SaveService _saveService;

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

        public void Init(SaveService saveService)
        {
            _saveService = saveService;
        }

        private void ResetProgress()
        {
            _saveService.ResetProcess();

            var restartService = new RestartGameService();
            restartService.Restart();
        }
    }
}