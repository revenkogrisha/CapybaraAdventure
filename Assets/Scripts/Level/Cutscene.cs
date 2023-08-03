using System.Collections;
using CapybaraAdventure.Other;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Level
{
    public class Cutscene : MonoBehaviour
    {
        private const string Cutscene0 = nameof(Cutscene0);
        private const string ZoomHero = nameof(ZoomHero);
        private const string ZoomDark = nameof(ZoomDark);

        [SerializeField] private Animator _cutsceneAnimator;

        private LoadingScreenProvider _loaderProvider;
        private readonly float _cutsceneChangeInterval = 1.5f;
        private int _cutscene0Id = Animator.StringToHash(Cutscene0);
        private int _zoomHeroId = Animator.StringToHash(ZoomHero);
        private int _zoomDarkId = Animator.StringToHash(ZoomDark);

        private void Start()
        {
            StartCoroutine(PerformCutscene());
        }

        [Inject]
        private void Construct(LoadingScreenProvider provider)
        {
            _loaderProvider = provider;
        }

        private IEnumerator PerformCutscene()
        {
            yield return new WaitForSeconds(_cutsceneChangeInterval);

            _cutsceneAnimator.SetBool(_zoomHeroId, true);

            yield return new WaitForSeconds(_cutsceneChangeInterval);

            _cutsceneAnimator.SetBool(_cutscene0Id, true);
            _cutsceneAnimator.SetBool(_zoomHeroId, false);

            yield return new WaitForSeconds(_cutsceneChangeInterval);

            _cutsceneAnimator.SetBool(_zoomDarkId, true);

            yield return new WaitForSeconds(_cutsceneChangeInterval);

            _cutsceneAnimator.SetBool(_cutscene0Id, false);

            yield return new WaitForSeconds(_cutsceneChangeInterval);

            LoadGame();
        }

        private async void LoadGame()
        {
            await _loaderProvider.LoadGameAsync();
        }
    }
}