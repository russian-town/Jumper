using TMPro;
using UnityEngine;

public class GameOverView : UIElement
{
    [SerializeField] private TMP_Text _progressText;

    public void ShowProgress(float progress)
    {
        _progressText.text = $"{progress}% completed";
    }
}
