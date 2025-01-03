using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CapybaraAdventure.Ad
{
    [DefaultExecutionOrder(0)]
    public class AdTimer : MonoBehaviour
    {
        public const int StartBlockPeriodInSeconds = 30;
        public const int BlockPeriodInSeconds = 45;

        public static AdTimer Instance { get; private set; }

        private bool _canShowAd = true;
        private bool _noAds = false;
        private CancellationToken _cancellationToken;

        public bool CanShowAd => _noAds == false && _canShowAd == true;

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
            BlockAdForPeriod(StartBlockPeriodInSeconds);
        }

        public void BlockAdForPeriod(int blockPeriod = BlockPeriodInSeconds)
        {
            BlockAd(blockPeriod, _cancellationToken).Forget();
        }

        public void SetNoAds(bool noAds)
        {
            _noAds = noAds;
        }

        private async UniTask BlockAd(int period, CancellationToken token)
        {
            _canShowAd = false;

            var delay = TimeSpan.FromSeconds(period);
            await Task.Delay(delay, token);

            _canShowAd = true;
        }
    }
}
