using System;
using UnityEngine;

namespace Core.Player
{
    public class HeroSkinSetter : MonoBehaviour
    {
        [Header("General Sprite Renderers")]
        [SerializeField] private SpriteRenderer _head;
        [SerializeField] private SpriteRenderer _body;

        [Header("Arms Sprite Renderers")]
        [SerializeField] private SpriteRenderer _leftArm;
        [SerializeField] private SpriteRenderer _rightArm;

        [Header("Legs Sprite Renderers")]
        [SerializeField] private SpriteRenderer _leftLeg;
        [SerializeField] private SpriteRenderer _rightLeg;

        [SerializeField] private SpriteRenderer _sword;
        
        public void Set(SkinPreset preset)
        {
            _head.sprite = preset.Head
                ?? throw new ArgumentNullException($"{nameof(_head)} sprite is null!");

            _body.sprite = preset.Body 
                ?? throw new ArgumentNullException($"{nameof(_body)} sprite is null!");

            _leftArm.sprite = preset.LeftArm 
                ?? throw new ArgumentNullException($"{nameof(_leftArm)} sprite is null!");

            _rightArm.sprite = preset.RightArm 
                ?? throw new ArgumentNullException($"{nameof(_rightArm)} sprite is null!");

            _leftLeg.sprite = preset.Leg 
                ?? throw new ArgumentNullException($"{nameof(_leftLeg)} sprite is null!");

            _rightLeg.sprite = preset.Leg 
                ?? throw new ArgumentNullException($"{nameof(_rightLeg)} sprite is null!");

            _sword.sprite = preset.Sword 
                ?? throw new ArgumentNullException($"{nameof(_sword)} sprite is null!");
        }
    }
}
