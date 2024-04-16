using System;
using Sourse.Level;
using Sourse.UI;
using Sourse.UI.Shop.Scripts.Buttons;
using UnityEngine;

public class HUD : UIElement
{
    [SerializeField] private PauseButton _pauseButton;
    [SerializeField] private LevelNumberText _currentlevelNumberText;
    [SerializeField] private LevelNumberText _nextlevelNumberText;

    public event Action PauseButtonClicked;

    public void Subscribe()
        => _pauseButton.AddListener(()
            => PauseButtonClicked?.Invoke());

    public void Unsubscribe()
        => _pauseButton.RemoveListener(()
            => PauseButtonClicked?.Invoke());

    public void Initialize(int levelNumber)
    {
        _pauseButton.Initialize();
        _currentlevelNumberText.Initialize(levelNumber);
        int nextLevelNumber = levelNumber++;
        _nextlevelNumberText.Initialize(nextLevelNumber);
    }
}
