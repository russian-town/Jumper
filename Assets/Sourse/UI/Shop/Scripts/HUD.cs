using Sourse.Game.FinishContent;
using Sourse.Game.Lose;
using Sourse.Ground;
using Sourse.Level;
using Sourse.PauseContent;
using Sourse.Player.Common.Scripts;
using Sourse.UI;
using Sourse.UI.LevelCompletePanel;
using Sourse.UI.Shop.Scripts;
using Sourse.UI.Shop.Scripts.Buttons;
using UnityEngine;

public class HUD : UIElement, IPauseHandler
{
    [SerializeField] private PauseButton _pauseButton;
    [SerializeField] private LevelNumberText _currentlevelNumberText;
    [SerializeField] private LevelNumberText _nextlevelNumberText;
    [SerializeField] private OpenableSkinBar _openableSkinBar;
    [SerializeField] private PauseView _pauseView;
    [SerializeField] private LevelFinishView _levelFinishView;
    [SerializeField] private LevelProgressView _levelProgressView;
    [SerializeField] private GameLossView _gameLossView;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private StartPanel _startPanel;

    private Pause _pause;
    private LevelLoader _levelLoader;

    public void Subscribe()
    {
        _pauseButton.AddListener(OnPauseButtonClicked);
        _levelFinishView.NextLevelButtonClicked += OnNextLevelButtonClicked;
        _gameLossView.RetryButtonClicked += OnRetryButtonClicked;
        _gameLossView.RewardedButtonClicked += OnRewardedButtonClicked;
        _gameLossView.CloseAdOfferScreenButtonClicked -= OnCloseAdOfferScreenButtonClicked;
        _levelProgressView.Subscribe();
        _levelFinishView.Subscribe();
        _gameLossView.Subscribe();
        _pauseView.Subscribe();
        _pauseView.ContinueButtonClicked += OnContinueButtonClicked;
        _pauseView.ExitButtonClicked += OnExitButtonClicked;
        _pauseView.RestatrButtonClicked += OnRestartButtonClicked;
    }

    public void Unsubscribe()
    {
        _pauseButton.RemoveListener(OnPauseButtonClicked);
        _levelFinishView.NextLevelButtonClicked -= OnNextLevelButtonClicked;
        _gameLossView.RetryButtonClicked -= OnRetryButtonClicked;
        _gameLossView.RewardedButtonClicked -= OnRewardedButtonClicked;
        _gameLossView.CloseAdOfferScreenButtonClicked -= OnCloseAdOfferScreenButtonClicked;
        _levelProgressView.Unsubscribe();
        _levelFinishView.Unsubscribe();
        _gameLossView.Unsubscribe();
        _pauseView.Unsubscribe();
        _pauseView.ContinueButtonClicked -= OnContinueButtonClicked;
        _pauseView.ExitButtonClicked -= OnExitButtonClicked;
        _pauseView.RestatrButtonClicked -= OnRestartButtonClicked;
        _openableSkinBar.Unsubscribe();
        _startPanel.Unsubscribe();
    }

    public void Initialize(int levelNumber,
        PlayerInitializer player,
        LevelProgress levelProgress,
        GameLoss gameLoss,
        Pause pause,
        OpenableSkinViewFiller openableSkinViewFiller,
        Finish finish,
        DeadZone deadZone,
        LevelLoader levelLoader)
    {
        _pauseButton.Initialize();
        _currentlevelNumberText.Initialize(levelNumber);
        int nextLevelNumber = levelNumber++;
        _nextlevelNumberText.Initialize(nextLevelNumber);
        _playerInput.Initialize(player.Animator);
        _levelProgressView.Initialize(
               levelProgress,
               finish,
               deadZone);
        _levelProgressView.UpdateProgressBar();
        _levelFinishView.Initialize(finish);
        _gameLossView.Initialize(gameLoss);
        _pauseView.Initialize(pause);
        _pause = pause;
        _levelLoader = levelLoader;
        _openableSkinBar.Initialize(openableSkinViewFiller);
        _startPanel.Initialize(_pause);
    }

    public void UpdateView()
        => _levelProgressView.UpdateProgressBar();

    public void SetPause(bool isPause)
        => _playerInput.SetPause(isPause);

    private void OnNextLevelButtonClicked()
        => _levelLoader.GoNext();

    private void OnRetryButtonClicked()
        => _levelLoader.Restart();

    private void OnRewardedButtonClicked()
    {
    }

    private void OnCloseAdOfferScreenButtonClicked()
        => _levelLoader.Restart();

    private void OnPauseButtonClicked()
        => _pause.Enable();

    private void OnContinueButtonClicked()
        => _pause.Disable();

    private void OnExitButtonClicked()
        => _levelLoader.ExitToMainMenu();

    private void OnRestartButtonClicked()
        => _levelLoader.Restart();
}
