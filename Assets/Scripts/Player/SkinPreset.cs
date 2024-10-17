using UnityEngine;

namespace Core.Player
{
    [CreateAssetMenu(fileName = "New Skin", menuName = "Presets/Hero Skin")]
    public class SkinPreset : ScriptableObject
    {
        [Header("General")]
        [SerializeField] private SkinName _name;
        [SerializeField] private Sprite _menuItem;

        [Space]
        [SerializeField] private Color _accentColor = Color.yellow;

        [Space]
        [SerializeField, Min(0)] private int _foodCost = 15;
        
        [Header("Upper Body Sprites")]
        [SerializeField] private Sprite _head;
        [SerializeField] private Sprite _body;

        [Header("Arm Sprites")]
        [SerializeField] private Sprite _leftArm;
        [SerializeField] private Sprite _rightArm;

        [Header("Leg Sprite")]
        [SerializeField] private Sprite _leg;

        [Header("Sword Sprite")]
        [SerializeField] private Sprite _sword;

        public SkinName Name => _name;
        public Sprite Head => _head;
        public Sprite MenuItem => _menuItem;

        public Color AccentColor => _accentColor;
        
        public int FoodCost => _foodCost;

        public Sprite Body => _body;
        public Sprite LeftArm => _leftArm;
        public Sprite RightArm => _rightArm;

        public Sprite Leg => _leg;
        public Sprite Sword => _sword;
    }
}