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

        public void BlockAdForStartPeriod()
        {
            BlockAd(StartBlockPeriodInSeconds).Forget(exc => throw exc);
        }

        public void BlockAdForPeriod()
        {
            BlockAd(BlockPeriodInSeconds).Forget(exc => throw exc);
        }

        private async UniTask BlockAd(int perid)
        {
            _canShowAd = false;
            await UniTask.WaitForSeconds(perid);
            _canShowAd = true;
        }
    }
}
