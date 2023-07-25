using UnityEngine;
using UnityEngine.UI;

public class PausePanel : UIElement
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    public void DisableButtons()
    {
        _restartButton.interactable = false;
        _exitButton.interactable = false;
    }
}
