using System;

namespace Core.Player
{
    [Flags]
    public enum SkinName
    {
        Capybuddy = 1 << 0,
        Greenbara = 1 << 1,
        Purplebara = 1 << 2,
        Pinkbara = 1 << 3,
        Darkbara = 1 << 4,
        Streetbara = 1 << 5,
        Redbara = 1 << 6,
        Sorrowbara = 1 << 7,
        Floralbara = 1 << 8
    }
}