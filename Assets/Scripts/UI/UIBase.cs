using UnityEngine;

namespace CapybaraAdventure.UI
{
    [DisallowMultipleComponent]
    public class UIBase : MonoBehaviour
    {
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
