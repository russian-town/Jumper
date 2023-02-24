using UnityEngine;
using UnityEngine.UI;

public class LevelProgressView : MonoBehaviour
{
    [SerializeField] private Image _levelProgressBar;

    public void UpdateProgressBar(float value)
    {
        _levelProgressBar.fillAmount = value;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
