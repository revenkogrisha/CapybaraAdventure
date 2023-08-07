using System.Collections;
using CapybaraAdventure.Other;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Level
{
    public class Cutscene : MonoBehaviour
    {
        private const string Cutscene0 = nameof(Cutscene0);

        [SerializeField] private Animator _cutsceneAnimator;

        private LoadingScreenProvider _loaderProvider;
        private readonly float _cutsceneChangeInterval = 2f;
        private int _cutscene0Id = Animator.StringToHash(Cutscene0);
//
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

            _cutsceneAnimator.SetBool(_cutscene0Id, true);

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