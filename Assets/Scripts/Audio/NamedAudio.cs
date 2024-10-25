using System;
using UnityEngine;

namespace Core.Audio
{
    [Serializable]
    public struct NamedAudio
    {
        public AudioName Name;
        public AudioClip Clip; 
    }
}