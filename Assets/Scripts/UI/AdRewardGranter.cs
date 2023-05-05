using UnityEngine;
using CapybaraAdventure.Ad;
using System;

namespace CapybaraAdventure.UI
{
    public class AdRewardGranter : MonoBehaviour
    {
        [SerializeField] private AppodealRewarded _rewardedAd;

        public event Action OnRewardGranted;

        #region MonoBehaviour

        private void OnEnable()
        {
            _rewardedAd.OnRewardGotten += GrantReward;
        }

        private void OnDisable()
        {
            _rewardedAd.OnRewardGotten -= GrantReward;
        }

        #endregion

        private void GrantReward() => OnRewardGranted?.Invoke();
    }
}