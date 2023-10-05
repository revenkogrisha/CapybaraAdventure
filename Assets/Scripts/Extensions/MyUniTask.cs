using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace CapybaraAdventure.Other
{
    public static class MyUniTask
    {
        public static UniTask Delay(int seconds)
        {
            return Delay((float)seconds);
        }

        public static UniTask Delay(float seconds)
        {
            TimeSpan delay = TimeSpan.FromSeconds(seconds);
            return UniTask.Delay(delay);
        }

        public static UniTask Delay(int seconds, CancellationToken token)
        {
            return Delay((float)seconds, token);
        }

        public static UniTask Delay(float seconds, CancellationToken token)
        {
            TimeSpan delay = TimeSpan.FromSeconds(seconds);
            return UniTask.Delay(delay, cancellationToken: token);
        }
    }
}