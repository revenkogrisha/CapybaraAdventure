using System;
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

        public static T GetRandomItem<T>(this Array array)
        {
            int random = Random.Range(0, array.Length);
            return (T)array.GetValue(random);
        }

        public static int GetRandomIndex(this Array array)
        {
            return Random.Range(0, array.Length);
        }
    }
}
