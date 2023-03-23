using UnityEngine;
using CapybaraAdventure.UI;
using Zenject;
using System.Threading.Tasks;

namespace CapybaraAdventure.Game
{
    public class AppStartup : MonoBehaviour
    {
        private MenuProvider _menuProvider;
        private GameMenu _menu;

        private void OnDisable()
        {
            if (_menu != null)
                _menu.OnMenuWorkHasOver -= UnloadMenu;
        }

        private async void Start()
        {
            await LoadAndRevealMenu();
        }

        private async Task LoadAndRevealMenu()
        {
            _menu = await _menuProvider.Load();
            _menu.OnMenuWorkHasOver += UnloadMenu;

            _menu.Reveal();
        }

        [Inject]
        private void Construct(MenuProvider menuProvider)
        {
            _menuProvider = menuProvider;
        }

        private void UnloadMenu() => _menuProvider.Unload();
    }
}