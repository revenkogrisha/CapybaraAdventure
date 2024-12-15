using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CapybaraAdventure.Other
{
    public class ScreenTweener
    {
        private const float DefaultLogoTargetY = 285f;

        private readonly float _logoShowDuration = 0.5f;
        private readonly float _buttonsDelay = 0.3f;
        private readonly float _buttonsScaleDuration = 0.4f;
        private readonly float _barScaleDuration = 0.6f;
        private readonly float _swordButtonFadeDuration = 4f;
        private readonly float _swordButtonFadeDelay = 4f;
        private readonly float _questBarScaleDuration = 0.4f;
        private readonly float _questBarDisplayDuration = 7f;
        private readonly float _questBarDisplayInterval = 10f;
        
        private bool _isQuestBarDisplaying = false;
        
        private readonly float _rewardsInterval = .8f;
        private readonly float _rewardsScaleDuration = .8f;

        public ScreenTweener() {  }

        public ScreenTweener(float commonShowDuration) : base()
        {
            _logoShowDuration = commonShowDuration;
            _buttonsScaleDuration = commonShowDuration;
        }

        public void TweenRewardBlocks(Transform[] blocks)
        {
            float preferredHeight = blocks[0].GetComponent<LayoutElement>().preferredHeight;
            float preferredWidth = blocks[0].GetComponent<LayoutElement>().preferredWidth;
            
            foreach (var block in blocks)
            {
                block.GetComponent<LayoutElement>().preferredWidth = 0f;
            }
            
            Sequence sequence = DOTween.Sequence();
            Vector2 preferredSize = new(preferredWidth, preferredHeight);
            foreach (var block in blocks)
            {
                block.gameObject.SetActive(true);
                sequence
                    .Append(block.GetComponent<LayoutElement>().DOPreferredSize(preferredSize, _rewardsScaleDuration).SetEase(Ease.OutBounce))
                    .AppendInterval(_rewardsInterval);
            }
        }

        public void FadeIn(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, _logoShowDuration);
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

        public void DisplayQuestBar(Transform bar)
        {
            if (_isQuestBarDisplaying == true || bar == null)
                return;

            _isQuestBarDisplaying = true;
            bar.localScale = Vector2.zero;
            bar.gameObject.SetActive(true);

            // NEW
            bar.DOScale(Vector2.one, _questBarScaleDuration);
        }

        public void TweenProgressBar(Transform transform)
        {
            transform.localScale = Vector2.zero;

            DOTween.Sequence()
                .AppendInterval(_buttonsDelay)
                .Append(transform.DOScale(Vector2.one, _barScaleDuration).SetEase(Ease.OutBounce));
        }
    }
}