using System;
using Sourse.Game.Lose;
using Sourse.Level;
using Sourse.PauseContent;
using Sourse.Player.Common.Scripts;
using Sourse.UI;
using Sourse.UI.LevelCompletePanel;
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

    public event Action PauseButtonClicked;

    public void Subscribe()
    {
        _pauseButton.AddListener(() => PauseButtonClicked?.Invoke());
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
        _pauseButton.RemoveListener(() => PauseButtonClicked?.Invoke());
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
    }

    public void Initialize(int levelNumber,
        PlayerInitializer player,
        LevelProgress levelProgress,
        GameLoss gameLoss,
        Pause pause)
    {
        _pauseButton.Initialize();
        _currentlevelNumberText.Initialize(levelNumber);
        int nextLevelNumber = levelNumber++;
        _nextlevelNumberText.Initialize(nextLevelNumber);
        _playerInput.Initialize(player.Animator);
        _levelProgressView.Initialize(
               levelProgress,
               player.Finisher,
               player.Death);
        _levelProgressView.UpdateProgressBar();
        _levelFinishView.Initialize(player.Finisher);
        _gameLossView.Initialize(gameLoss);
        _pauseView.Initialize(pause);
        //_openableSkinBar.Initialize();
    }

    public void UpdateView()
        => _levelProgressView.UpdateProgressBar();

    public void SetPause(bool isPause)
    {
        _playerInput.SetPause(isPause);
    }

    private void OnNextLevelButtonClicked()
    {
    }

    private void OnRetryButtonClicked()
    {
    }

    private void OnRewardedButtonClicked()
    {
    }

    private void OnCloseAdOfferScreenButtonClicked()
    {
    }

    private void OnContinueButtonClicked()
    {
    }

    private void OnExitButtonClicked()
    {
    }

    private void OnRestartButtonClicked()
    {
    }
}
