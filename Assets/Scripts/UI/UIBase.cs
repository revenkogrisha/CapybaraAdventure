using Core.Audio;
using UnityEngine;

namespace CapybaraAdventure.UI
{
    [DisallowMultipleComponent]
    public class UIBase : MonoBehaviour
    {
        public void InitAudioHandler(IAudioHandler audioHandler)
        {
            UIButton[] buttons = GetComponentsInChildren<UIButton>();
            foreach (UIButton button in buttons)
            {
                button.AudioHandler = audioHandler;
            }
        }

        public virtual void Reveal()
        {
            gameObject.SetActive(true);       
        }

        public virtual void Conceal()
        {
            gameObject.SetActive(false);
        }
    }
}
