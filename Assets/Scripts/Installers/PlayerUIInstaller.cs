using UnityEngine;
using Zenject;
using CapybaraAdventure.UI;

namespace CapybaraAdventure.Installers
{
    public class PlayerUIInstaller : MonoInstaller
    {
        [SerializeField] private JumpSlider _slider;
        [SerializeField] private JumpButton _button;

        public override void InstallBindings()
        {
            BindJumpSlider();
            BindJumpButton();
        }

        private void BindJumpSlider()
        {
            Container
                .Bind<JumpSlider>()
                .FromInstance(_slider)
                .AsSingle();
        }

        private void BindJumpButton()
        {
            Container
                .Bind<JumpButton>()
                .FromInstance(_button)
                .AsSingle();
        }
    }
}
