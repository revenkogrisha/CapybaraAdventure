using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class HeightCheckService
    {
        private readonly Collider2D _collider;
        private readonly LayerMask _testLayer;
        private readonly float _heightTest;

        public HeightCheckService(
            Collider2D collider, 
            LayerMask layer, 
            float testRadius)
        {
            _collider = collider;
            _testLayer = layer;

            _heightTest = _collider.bounds.extents.y + testRadius;
        }

        public bool IsNotGrounded
        {
            get
            {
                var colliderCenter = _collider.bounds.center;
                var hit = Physics2D.Raycast(colliderCenter, Vector2.down, _heightTest, _testLayer);

                return hit.collider == null;
            }
        }
    }
}
