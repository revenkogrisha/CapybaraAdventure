using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CapybaraAdventure.Other
{
    public class LocalAssetLoader
    {
        private GameObject _cachedObject;

        public bool IsCached => _cachedObject != null;

        protected async Task<T> LoadInternal<T>(
            string assetID,
            Transform parent = null)
        {
            var handle = Addressables.InstantiateAsync(assetID, parent);
            _cachedObject = await handle.Task;

            T component = _cachedObject.GetComponent<T>();
                
            if (component == null)
                throw new NullReferenceException(
                    $"Object of type {typeof(T)} is null while loading an asset from Addressables");
                
            return component;
                
        }

        protected void UnlodadInternalIfCached()
        {
            if (IsCached == true)
                UnlodadInternal();
        }

        protected void UnlodadInternal()
        {
            if (_cachedObject == null)
                throw new NullReferenceException("Nothing to unload. Cached object is null!");

            _cachedObject.SetActive(false);
            Addressables.ReleaseInstance(_cachedObject);
            _cachedObject = null;
        }
    }
}