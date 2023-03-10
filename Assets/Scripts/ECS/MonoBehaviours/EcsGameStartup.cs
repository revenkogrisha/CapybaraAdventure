using Leopotam.Ecs;
using UnityEngine;

namespace CapybaraAdventure
{
    public class EcsGameStartup : MonoBehaviour
    {
        private EcsWorld _world = null;
        private EcsSystems _systems = null;

        #region MonoBehaviour

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            AddSystems();

            _systems.Init();
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            _systems.Destroy();
            _systems = null;

            _world.Destroy();
            _world = null;
        }

        #endregion

        private void AddSystems()
        {

        }
    }
}
