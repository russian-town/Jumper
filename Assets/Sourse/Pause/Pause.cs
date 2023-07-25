using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private PauseButton _pauseButton;
    [SerializeField] private PausePanel _pausePanel;

    private List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();
    private Game _game;

    public void Initialize(IPauseHandler[] pauseHandlers, Game game)
    {
        _pauseHandlers.AddRange(pauseHandlers);
        _game = game;
    }

    public void Enable()
    {
        if (_game.IsLevelComplete == true)
            _pausePanel.DisableButtons();

        _pauseButton.Hide();
        _pausePanel.Show();

        foreach (var pauseHandler in _pauseHandlers)
        {
            pauseHandler.SetPause(true);
        }
    }

    public void Disable()
    {
        _pauseButton.Show();
        _pausePanel.Hide();

        foreach (var pauseHandler in _pauseHandlers)
        {
            pauseHandler.SetPause(false);
        }
    }
}
