using UnityEngine;

namespace CapybaraAdventure.Level
{
    public class Platform : MonoBehaviour
    {
        public const float Length = 30f;

        [Header("Markers")]
        [SerializeField] private FoodSpawnMarker[] _foodMarkers;
        [SerializeField] private ChestSpawnMarker[] _chestMarkers;
        [SerializeField] private TreasureChestSpawnMarker[] _treasureChestMarkers;
        [SerializeField] private EnemySpawnMarker[] _enemyMarkers;

        public FoodSpawnMarker[] FoodMarkers => _foodMarkers;
        public ChestSpawnMarker[] ChestMarkers => _chestMarkers;
        public TreasureChestSpawnMarker[] TreasureChestMarkers => _treasureChestMarkers;
        public EnemySpawnMarker[] EnemyMarkers => _enemyMarkers;

        protected void OnDrawGizmos()
        {
            float halfWidth = 15f;

            Color gizmosColor = Color.red;

            Gizmos.color = gizmosColor;

            Vector3 center = transform.position;
            Vector3 leftTop = center + Vector3.left * halfWidth + Vector3.up;
            Vector3 leftBottom = center + Vector3.left * halfWidth + Vector3.down;
            Vector3 rightTop = center + Vector3.right * halfWidth + Vector3.up;
            Vector3 rightBottom = center + Vector3.right * halfWidth + Vector3.down;

            Gizmos.DrawLine(leftTop, leftBottom);
            Gizmos.DrawLine(rightTop, rightBottom);
            Gizmos.DrawLine(leftTop, rightTop);
            Gizmos.DrawLine(leftBottom, rightBottom);
        }
    }
}
