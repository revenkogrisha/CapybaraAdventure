using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Level
{
    public class BackgroundHandler : IDisposable
    {
        private readonly Camera _camera;
        private readonly Transform _root;
        private readonly Background _backgroundPrefab;
        private Background _backgroundInstance;

        public BackgroundHandler(Background backgroundPrefab)
        {
            _camera = Camera.main;
            _root = _camera.transform;
            _backgroundPrefab = backgroundPrefab;
        }

        public void Dispose()
        {
            if (_backgroundInstance != null)
                Object.Destroy(_backgroundInstance.gameObject);
        }

        public void CreateBackground(Vector2 startPosition, BackgroundPreset preset)
        {
            _backgroundInstance = Object.Instantiate(_backgroundPrefab);
            
            _backgroundInstance.transform.SetParent(_root);
            _backgroundInstance.transform.localPosition = startPosition;
            
            _backgroundInstance.ApplyPreset(preset);

            _camera.backgroundColor = preset.UndercoverColor;
        }
    }
}
