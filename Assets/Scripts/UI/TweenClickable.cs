using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CapybaraAdventure.UI
{
    public class TweenClickable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private const float ClickScale = 0.85f;
        private const float ClickTweenDuration = 0.08f;
        
        [SerializeField] private bool _isTweenClickable = true;
        
        private bool _isTweeningScale = false;
        
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (_isTweeningScale == false && _isTweenClickable == true)
            {
                _isTweeningScale = true;
                transform.DOScale(ClickScale, ClickTweenDuration);
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (_isTweeningScale == true && _isTweenClickable == true)
            {
                transform.DOScale(1f, ClickTweenDuration);
                _isTweeningScale = false;
            }
        }
    }
}
