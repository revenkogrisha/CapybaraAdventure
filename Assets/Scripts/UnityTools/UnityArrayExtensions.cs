using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace UnityTools
{
    public static class UnityArrayExtensions
    {
        public static void TryShuffleArray<T>(this Array array)
        {
            if (array.Length < 2)
                return;

            var newArray = array.Clone() as T[];
            for (int i = 0; i < newArray.Length; i++)
            {
                var temp = newArray[i];
                var random = Random.Range(0, newArray.Length);

                newArray[i] = newArray[random];
                newArray[random] = temp;
            }

            Array.Copy(newArray, array, array.Length);
        }
    }
}
