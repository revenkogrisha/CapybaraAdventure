using DG.Tweening;
using UnityEngine;

namespace CapybaraAdventure.Other
{
    public class ScreenTweener
    {
        private const float DefaultLogoTargetY = 350f;

        private readonly float _logoShowDuration = 0.5f;
        private readonly float _buttonsDelay = 0.3f;
        private readonly float _buttonsScaleDuration = 0.4f;
        private readonly float _swordButtonFadeDuration = 4f;
        private readonly float _swordButtonFadeDelay = 4f;
        private readonly float _questBarScaleDuration = 0.4f;
        private readonly float _questBarDisplayDuration = 7f;
        private readonly float _questBarDisplayInterval = 25f;
        private bool _isQuestBarDisplaying = false;

        public ScreenTweener() {  }

        public ScreenTweener(float commonShowDuration) : base()
        {
            _logoShowDuration = commonShowDuration;
            _buttonsScaleDuration = commonShowDuration;
        }

        public void TweenLogo(
            Transform logo,
            float logoTargetY = DefaultLogoTargetY)
        {
            logo.localScale = Vector2.zero;

            logo.DOScale(Vector2.one, _logoShowDuration);
            logo.DOLocalMoveY(logoTargetY, _logoShowDuration);
        }

        public void ScaleTweenLogo(Transform logo)
        {
            logo.localScale = Vector2.zero;

            logo.DOScale(Vector2.one, _logoShowDuration);
        }

        public void TweenButton(Transform button)
        {
            button.localScale = Vector2.zero;

            DOTween.Sequence()
                .AppendInterval(_buttonsDelay)
                .Append(button.DOScale(Vector2.one, _buttonsScaleDuration));
        }

        public void TweenButtonWithoutDelay(Transform button)
        {
            button.localScale = Vector2.zero;

            DOTween.Sequence()
                .Append(button.DOScale(Vector2.one, _buttonsScaleDuration));
        }

        public void TweenSwordButton(Transform button)
        {
            DOTween.Sequence()
                .Append(button.DOScale(Vector2.one, _buttonsScaleDuration))
                .AppendInterval(_swordButtonFadeDelay)
                .Append(button.DOScale(Vector2.zero, _swordButtonFadeDuration))
                .AppendCallback(() => button.gameObject.SetActive(false));
        }

        public void DisplayQuestBarForPeriod(Transform bar)
        {
            if (_isQuestBarDisplaying == true || bar == null)
                return;

            _isQuestBarDisplaying = true;
            bar.localScale = Vector2.zero;
            bar.gameObject.SetActive(true);

            DOTween.Sequence()
                .Append(bar.DOScale(Vector2.one, _questBarScaleDuration))
                .AppendInterval(_questBarDisplayDuration)
                .Append(bar.DOScale(Vector2.zero, _questBarScaleDuration))
                .AppendCallback(() => bar.gameObject.SetActive(false))
                .AppendCallback(() => _isQuestBarDisplaying = false)
                .AppendInterval(_questBarDisplayInterval)
                .AppendCallback(() => DisplayQuestBarForPeriod(bar));


        }
    }
}