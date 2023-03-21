using UnityEngine;
using CapybaraAdventure.UI;
using Zenject;

namespace CapybaraAdventure.Game
{
    public class AppStartup : MonoBehaviour
    {
        private MenuProvider _menuProvider;

        private async void Start()
        {
            await _menuProvider.Load();
        }

        [Inject]
        private void Construct(MenuProvider menuProvider)
        {
            _menuProvider = menuProvider;
        } 
    }
}
