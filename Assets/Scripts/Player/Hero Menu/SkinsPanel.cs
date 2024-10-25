using System;
using System.Collections.Generic;
using System.Linq;
using Core.Player;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.UI
{
    public class SkinsPanel : MonoBehaviour
    {
        [SerializeField] private SkinItemView _itemPrefab;

        [Space]
        [SerializeField] private Transform _itemsRoot;
        
        private List<SkinItemView> _items;
        
        public event Action<SkinName> ItemDisplayCommand;

        public void CreateItems(IEnumerable<SkinPreset> presets)
        {
            if (_items == null)
                _items = new(presets.Count());
            else if (_items.Count > 0)
                ClearItems();

            foreach (SkinPreset preset in presets)
            {
                SkinItemView item = NightPool.Spawn(_itemPrefab, _itemsRoot);

                item.Initialize(this, preset.Name, preset.MenuItem);
                item.SetSelected(false);

                _items.Add(item);
            }
        }

        public void CommandItemDisplay(SkinName skinName) => 
            ItemDisplayCommand?.Invoke(skinName);

        public void SetSelected(SkinName skinName, bool state)
        {
            SkinItemView selected = _items.Single(item => item.Name == skinName);

            selected.SetSelected(state);
            selected.transform.SetAsFirstSibling();
        }

        private void ClearItems()
        {
            foreach (SkinItemView item in _items)
                NightPool.Despawn(item);

            _items.Clear();
        }
    }
}