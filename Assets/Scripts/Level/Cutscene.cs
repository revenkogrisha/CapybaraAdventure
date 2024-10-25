using CapybaraAdventure.Other;
using CapybaraAdventure.Player;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;
using System.Threading;
using CapybaraAdventure.UI;

namespace CapybaraAdventure.Level
{
    public class Cutscene : MonoBehaviour
    {
        private const string Cutscene0 = nameof(Cutscene0);
        private const string ZoomHero = nameof(ZoomHero);
        private const string ZoomDark = nameof(ZoomDark);

        [SerializeField] private Animator _cutsceneAnimator;
        [SerializeField] private UIButton _skipButton;

        private CancellationToken _cancellationToken;
        private readonly float _cutsceneChangeInterval = 1.5f;
        private int _cutscene0Id = Animator.StringToHash(Cutscene0);
        private int _zoomHeroId = Animator.StringToHash(ZoomHero);
        private int _zoomDarkId = Animator.StringToHash(ZoomDark);

        #region Injected fields
        private LoadingScreenProvider _loaderProvider;
        private PlayerData _playerData;
        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            _cancellationToken = this.GetCancellationTokenOnDestroy();
        }
        
        private void OnEnable()
        {
            _skipButton.OnClicked += LoadGame;
        }
        
        private void OnDisable()
        {
            _skipButton.OnClicked -= LoadGame;
        }

        private void Start()
        {
            if (_playerData.IsCutsceneWatched == true)
                _skipButton.gameObject.SetActive(true);

            PerformCutscene(_cancellationToken).Forget();
        }
        
        #endregion

        [Inject]
        private void Construct(LoadingScreenProvider provider, PlayerData playerData)
        {
            _loaderProvider = provider;
            _playerData = playerData;
        }

        private async UniTask PerformCutscene(CancellationToken token)
        {
            await MyUniTask.Delay(_cutsceneChangeInterval, token);

            _cutsceneAnimator.SetBool(_zoomHeroId, true);

            await MyUniTask.Delay(_cutsceneChangeInterval, token);

            _cutsceneAnimator.SetBool(_cutscene0Id, true);
            _cutsceneAnimator.SetBool(_zoomHeroId, false);

            await MyUniTask.Delay(_cutsceneChangeInterval, token);

            _cutsceneAnimator.SetBool(_zoomDarkId, true);

            await MyUniTask.Delay(_cutsceneChangeInterval, token);

            _cutsceneAnimator.SetBool(_cutscene0Id, false);

            await MyUniTask.Delay(_cutsceneChangeInterval, token);

            _playerData.IsCutsceneWatched = true;

            LoadGame();
        }

        private async void LoadGame()
        {
            await _loaderProvider.LoadGameAsync();
        }
    }
}