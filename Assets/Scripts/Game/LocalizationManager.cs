using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace CapybaraAdventure.Game
{
    public class LocalizationManager
    {
        private int _currentLanguageIndex = 0;

        private List<Locale> AvailableLocales => LocalizationSettings.AvailableLocales.Locales;

        public int CurrentLanguageIndex => _currentLanguageIndex;

        public async void NextLanguage()
        {
            _currentLanguageIndex++;
            if (_currentLanguageIndex >= AvailableLocales.Count)
                _currentLanguageIndex = 0;

            await SetLanguage(_currentLanguageIndex);
        }

        public async UniTask SetLanguage(int index)
        {
            await UniTask.WaitUntil(() => AvailableLocales.Count > 0);
            LocalizationSettings.SelectedLocale = AvailableLocales[index];
            _currentLanguageIndex = index;
        }
    }
}
