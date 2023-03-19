using UnityEngine;

namespace CapybaraAdventure.UI
{
    [DisallowMultipleComponent]
    public class UIBase : MonoBehaviour
    {
        public void Reveal()
        {
            gameObject.SetActive(true);       
        }

        public void Conceal()
        {
            gameObject.SetActive(false);
        }
    }
}
