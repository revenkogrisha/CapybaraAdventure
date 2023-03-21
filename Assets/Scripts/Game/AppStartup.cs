using UnityEngine;
using CapybaraAdventure.UI;
using Zenject;

namespace CapybaraAdventure.Game
{
    public class AppStartup : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private DiContainer _diContainer;

        private async void Start()
        {
            var menuProvider = new MenuProvider(_canvas, _diContainer);
            await menuProvider.Load();
        }

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        } 
    }
}
