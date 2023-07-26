using DG.Tweening;
using UnityEngine;

namespace CapybaraAdventure.Other
{
    public class ScreenTweener
    {
        private const float DefaultLogoTargetY = 245f;

        private readonly float _logoShowDuration = 0.5f;
        private readonly float _buttonsDelay = 0.3f;
        private readonly float _buttonsScaleDuration = 0.4f;
        private readonly float _swordButtonFadeDuration = 4f;
        private readonly float _swordButtonFadeDelay = 4f;

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
            logo.DOLocalMoveY(DefaultLogoTargetY, _logoShowDuration);
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
            button.localScale = Vector2.zero;

            DOTween.Sequence()
                .Append(button.DOScale(Vector2.one, _buttonsScaleDuration))
                .AppendInterval(_swordButtonFadeDelay)
                .Append(button.DOScale(Vector2.zero, _swordButtonFadeDuration))
                .AppendCallback(() => button.gameObject.SetActive(false));
        }
    }
}