using TMPro;
using UnityEngine;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private TMP_Text _progressText;

    public void ShowPanel(float progress)
    {
        gameObject.SetActive(true);
        _progressText.text = $"{progress}% completed";
    }
}
