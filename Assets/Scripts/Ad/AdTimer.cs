using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CapybaraAdventure.Ad
{
    public class AdTimer : MonoBehaviour
    {
        public const int StartBlockPeriodInSeconds = 30;
        public const int BlockPeriodInSeconds = 45;

        public static AdTimer Instance { get; private set; }

        private bool _canShowAd = true;
        private CancellationToken _cancellationToken;

        public bool CanShowAd => _canShowAd;

        private void Awake()
        {
            if (Instance == this)
                return;
            else if (Instance != null)
                throw new InvalidOperationException("AdTimer is a singleton. There can be only one instance of it.");

            _cancellationToken = this.GetCancellationTokenOnDestroy();

            Instance = this;
        }

        private void Start()
        {
            BlockAd(StartBlockPeriodInSeconds).Forget();
        }

        public void BlockAdForPeriod()
        {
            BlockAd(BlockPeriodInSeconds).Forget();
        }

        private async UniTask BlockAd(int period)
        {
            _canShowAd = false;

            var delay = TimeSpan.FromSeconds(period);
            await Task.Delay(delay, _cancellationToken);

            _canShowAd = true;
        }
    }
}
