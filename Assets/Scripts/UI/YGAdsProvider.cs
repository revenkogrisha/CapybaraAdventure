using YG;
using UnityEngine;

namespace CapybaraAdventure.UI
{
    public static class YGAdsProvider
    {
        public static void TryShowFullscreenAdWithChance(int chance)
        {
            var random = Random.Range(0, 101);

            if (chance < random)
                return;

            YandexGame.FullscreenShow();
        }
    
        public static void ShowRewardedAd(int id) => YandexGame.RewVideoShow(id);
    }
}
