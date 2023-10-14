using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using System.Collections.Generic;

namespace CapybaraAdventure.Game
{
    public class LocalizationManager : MonoBehaviour
    {
        private int _currentLanguageIndex = 0;

        private List<Locale> AvaliableLocales => LocalizationSettings.AvailableLocales.Locales;

        private void Start()
        {
            SetLanguage(0);
        }

        public void NextLanguage()
        {
            _currentLanguageIndex++;
            if (_currentLanguageIndex >= AvaliableLocales.Count)
                _currentLanguageIndex = 0;

            SetLanguage(_currentLanguageIndex);
        }

        private void SetLanguage(int index)
        {
            LocalizationSettings.SelectedLocale = AvaliableLocales[index];
        }
    }
}
