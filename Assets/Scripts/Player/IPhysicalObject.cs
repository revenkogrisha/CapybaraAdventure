using UnityEngine;

namespace CapybaraAdventure.Player
{
    public interface IPhysicalObject
    {
        public Transform Transform { get; }
        public Rigidbody2D Rigidbody2D { get; }
    }
}
