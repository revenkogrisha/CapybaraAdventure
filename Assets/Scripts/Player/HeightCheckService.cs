using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class HeightCheckService
    {
        private readonly Collider2D _collider;
        private readonly LayerMask _testLayer;
        private readonly Transform _groundCheckTransform;
        private readonly float _groundCheckRadius;
        private readonly float _heightTest;

        public HeightCheckService(
            Collider2D collider, 
            LayerMask layer, 
            float testRadius,
            Transform groundCheckTransform,
            float groundCheckRadius)
        {
            _collider = collider;
            _testLayer = layer;
            _groundCheckTransform = groundCheckTransform;
            _groundCheckRadius = groundCheckRadius;

            _heightTest = _collider.bounds.extents.y + testRadius;
        }

        public bool IsNotGrounded
        {
            get
            {
                Vector3 colliderCenter = _collider.bounds.center;
                RaycastHit2D hit = Physics2D.Raycast(colliderCenter, Vector2.down, _heightTest, _testLayer);
                
                Collider2D ground = Physics2D.OverlapCircle(_groundCheckTransform.position, _groundCheckRadius, _testLayer);

                return hit.collider == null && ground == null;
            }
        }
    }
}
