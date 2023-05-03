using UnityEngine;
using CapybaraAdventure.Ad;

namespace CapybaraAdventure.UI
{
    public abstract class AdRewardGranter : MonoBehaviour
    {
        [SerializeField] private AppodealRewarded _rewardedAd;

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

        protected abstract void GrantReward();
    }
}