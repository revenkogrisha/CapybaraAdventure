using UnityEngine;
using UnityTools.Buttons;

namespace CapybaraAdventure.UI
{
    public class SocialMediaLinker : MonoBehaviour
    {
        public const string TelegramUrl = "https://t.me/GrishaMakingGames";
        public const string YouTubeUrl = "https://www.youtube.com/@GrishaMakingGames/featured";

        [SerializeField] private UIButton _telegramButton;
        [SerializeField] private UIButton _youtubeButton;

        #region MonoBehaviour

        private void OnEnable()
        {
            _telegramButton.OnClicked += OpenTelegramUrl;   
            _youtubeButton.OnClicked += OpenYoutubeUrl;   
        }

        private void OnDisable()
        {
            _telegramButton.OnClicked -= OpenTelegramUrl;   
            _youtubeButton.OnClicked -= OpenYoutubeUrl;   
        }

        #endregion

        private void OpenTelegramUrl() => Application.OpenURL(TelegramUrl);

        private void OpenYoutubeUrl() => Application.OpenURL(YouTubeUrl);
    }
}