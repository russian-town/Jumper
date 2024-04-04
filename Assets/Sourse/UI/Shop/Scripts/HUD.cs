using System;
using Sourse.UI;
using Sourse.UI.Shop.Scripts.Buttons;
using UnityEngine;

public class HUD : UIElement
{
    [SerializeField] private PauseButton _pauseButton;

    public event Action PauseButtonClicked;

    public void Subscribe()
        => _pauseButton.AddListener(()
            => PauseButtonClicked?.Invoke());

    public void Unsubscribe()
        => _pauseButton.RemoveListener(()
            => PauseButtonClicked?.Invoke());

    public void Initialize()
        => _pauseButton.Initialize();
}
