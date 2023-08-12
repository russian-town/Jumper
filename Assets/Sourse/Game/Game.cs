using UnityEngine;

[RequireComponent(typeof(OpenableSkinHandler))]
public class Game : MonoBehaviour, IPauseHandler
{
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private LevelProgressView _levelProgressView;
    [SerializeField] private Level _level;
    [SerializeField] private LevelCompletePanel _levelCompletePanel;
    [SerializeField] private float _percentOpeningSkin;
    [SerializeField] private int _moneyOfLevel;
    [SerializeField] private Pause _pause;
    [SerializeField] private RetryButton _retryButton;
    [SerializeField] private RewardedPanel _rewardedPanel;
    [SerializeField] private PauseButton _pauseButton;
    [SerializeField] private NoThanksButton _noThanksButton;
    [SerializeField] private RewardedButton _rewardedButton;
    [SerializeField] private NextLevelButton _nextLevelButton;
    [SerializeField] private Wallet _wallet;

    private OpenableSkinHandler _openableSkinHandler;
    private Player _player;
    private LevelProgress _levelProgress;
    private PlayerInput _playerInput;
    private bool _isPause;
    private PlayerPosition _playerLastPosition;
    private PlayerPosition _playerStartPosition;
    private YandexAds _yandexAds;
    private ApplicationStatusChecker _applicationStatusChecker;

    private bool _isStart;
    private bool _isRewarded = false;

    public bool IsLevelComplete { get; private set; }

    private void OnDisable()
    {
        if (_player == null)
            return;

        _isStart = false;
        _player.Died -= OnPlayerDied;
        _player.LevelCompleted -= OnLevelCompleted;
        _playerInput.Tap -= StartGame;
        _yandexAds.OpenInterstitialCallback -= OnOpenInterstitialCallback;
        _yandexAds.CloseInterstitialCallback -= OnCloseInterstitialCallback;
        _gameOverView.Hide();
    }

    public void Initialaize(Player player, LevelProgress levelProgress, PlayerInput playerInput, PlayerPosition playerStartPosition, ApplicationStatusChecker applicationStatusChecker)
    {
        _openableSkinHandler = GetComponent<OpenableSkinHandler>();
        _yandexAds = new YandexAds();
        _applicationStatusChecker = applicationStatusChecker;
        _applicationStatusChecker.Initialize(_yandexAds);
        _yandexAds.OpenInterstitialCallback += OnOpenInterstitialCallback;
        _yandexAds.CloseInterstitialCallback += OnCloseInterstitialCallback;
        _openableSkinHandler.Initialize(_levelCompletePanel);
        _player = player;
        _playerInput = playerInput;
        _playerStartPosition = playerStartPosition;
        player.Died += OnPlayerDied;
        player.LevelCompleted += OnLevelCompleted;
        _playerInput.Tap += StartGame;
        _levelProgress = levelProgress;
        _nextLevelButton.Initialize();
        _rewardedButton.Initialize();
        _noThanksButton.Initialize();
        _retryButton.Initialize();
        _pause.Initialize(new IPauseHandler[] { player, this, _nextLevelButton, _retryButton, _noThanksButton, _rewardedButton }, this);
    }

    public void StartGame()
    {
        if (_isStart == true || _isPause == true)
            return;

        _isStart = true;
        _player.SetStart(_isStart);
        _levelProgressView.Show();
        _gameOverView.Hide();
    }

    public void SetPause(bool isPause)
    {
        _isPause = isPause;

        if (_isPause == true && IsLevelComplete == true)
            _nextLevelButton.Hide();
        else
            _nextLevelButton.Show();

        if (_isStart == false && _isPause == true)
            _retryButton.Hide();
        else if (_isStart == false && _isPause == true && _isRewarded == true)
            _retryButton.Hide();
        else if (_isPause == false && _isRewarded == false)
            _retryButton.Show();
    }

    public void SetLastPosition(PlayerPosition playerLastPosition)
    {
        _playerLastPosition = playerLastPosition;
    }

    private void OnOpenInterstitialCallback()
    {
        _applicationStatusChecker.ChangeSoundStatus(true);
    }

    private void OnCloseInterstitialCallback(bool isClose)
    {
        _applicationStatusChecker.ChangeSoundStatus(false);
    }

    private void OnPlayerDied()
    {
        _isStart = false;
        _levelProgress.DeleteSavedDistance();
        float percent = Mathf.Ceil(_levelProgress.CurrentDistance * 100f);
        _gameOverView.Show();

        if (_playerLastPosition == null || _playerLastPosition == _playerStartPosition)
        {
            _rewardedPanel.Hide();

            if (!_isPause)
                _retryButton.Show();
        }
        else
        {
            _isRewarded = true;
            _rewardedPanel.Show();
            _retryButton.Hide();
        }

        _gameOverView.ShowProgress(percent);
        _levelProgressView.Hide();
    }

    private void OnLevelCompleted()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        _yandexAds.ShowInterstitial();
#endif
        IsLevelComplete = true;
        _levelProgress.DeleteSavedDistance();
        _levelProgressView.Hide();
        _levelCompletePanel.Show();
        _wallet.AddMoney(_moneyOfLevel);

        if (_openableSkinHandler.GetOpenableSkin() != null)
        {
            _levelCompletePanel.Initialize(_openableSkinHandler.GetOpenableSkin());
            _levelCompletePanel.StartFillSkinBar(_percentOpeningSkin);
        }
        else
        {
            _levelCompletePanel.HideOpeningSkinBar();
        }

        _levelCompletePanel.SetText(_level.CurrentLevelNumber);
    }
}
