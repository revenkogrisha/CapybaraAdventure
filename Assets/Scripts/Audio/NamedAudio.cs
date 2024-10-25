using System;
using TriInspector;
using UnityEngine;

namespace Core.Audio
{
    [Serializable]
    public struct NamedAudio
    {
        public AudioName Name;
        [Required] public AudioClip Clip; 
    }
}