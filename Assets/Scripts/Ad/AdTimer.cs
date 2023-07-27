using System;
using System.Collections;
using UnityEngine;

namespace CapybaraAdventure.Ad
{
    public class AdTimer : MonoBehaviour
    {
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

        public void BlockAdForPeriod()
        {
            StartCoroutine(BlockAd());
        }

        private IEnumerator BlockAd()
        {
            _canShowAd = false;
            yield return new WaitForSeconds(BlockPeriodInSeconds);
            _canShowAd = true;
        }
    }
}
