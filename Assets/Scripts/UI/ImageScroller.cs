using UnityEngine;
using UnityEngine.UI;

namespace Core.Common
{
    public class ImageScroller : MonoBehaviour
    {
        [SerializeField] private RawImage _image;

        [Space]
        [SerializeField] private Vector2 _scollDirection = new(-0.5f, -1f);
        [SerializeField] private float _speedMultiplier = 0.004f;

        private void FixedUpdate()
        {
            Rect rect = _image.uvRect;
            rect.position = new Vector2(
                rect.position.x + _scollDirection.x * _speedMultiplier,
                rect.position.y + _scollDirection.y * _speedMultiplier);

            _image.uvRect = rect;
        }        
    }
}
