using System;
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

        public bool CanShowAd => _canShowAd;

        private void Awake()
        {
            if (Instance == this)
                return;
            else if (Instance != null)
                throw new InvalidOperationException("AdTimer is a singleton. There can be only one instance of it.");

            Instance = this;
        }

        private void Start()
        {
            BlockAdForStartPeriod();
        }

        public async void BlockAdForStartPeriod()
        {
            await BlockAd(StartBlockPeriodInSeconds);
        }

        public async void BlockAdForPeriod()
        {
            await BlockAd(BlockPeriodInSeconds);
        }

        private async UniTask BlockAd(int perid)
        {
            _canShowAd = false;
            await UniTask.WaitForSeconds(perid);
            _canShowAd = true;
        }
    }
}
