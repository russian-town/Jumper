using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelCompletePanel : UIElement
{
    public event UnityAction<int> SkinOpened;

    private const string FillAmountKey = "FillAmount";

    private const float MaxPercent = 1f;

    [SerializeField] private float _speed;
    [SerializeField] private Image _openingSkinBar;
    [SerializeField] private Image _openingSkinBarBackground;

    private int _id;

    public void Initialize(Skin skin)
    {
        _openingSkinBar.sprite = skin.Icon;
        _openingSkinBarBackground.color = Color.black;
        _openingSkinBarBackground.sprite = skin.Icon;
        _id = skin.ID;
    }

    public void HideOpeningSkinBar()
    {
        _openingSkinBar.enabled = false;
        _openingSkinBarBackground.enabled = false;
    }

    public void StartFillSkinBar(float percent)
    {
        StartCoroutine(FillSkinBar(percent));
    }

    private IEnumerator FillSkinBar(float percent)
    {
        float targetFillAmount;

        if (PlayerPrefs.HasKey(FillAmountKey) == true)
            targetFillAmount = PlayerPrefs.GetFloat(FillAmountKey) + percent;
        else
            targetFillAmount = _openingSkinBar.fillAmount + percent;

        while (_openingSkinBar.fillAmount != targetFillAmount)
        {
            _openingSkinBar.fillAmount = Mathf.MoveTowards(_openingSkinBar.fillAmount, targetFillAmount, _speed * Time.deltaTime);
            PlayerPrefs.SetFloat(FillAmountKey, _openingSkinBar.fillAmount);

            if (_openingSkinBar.fillAmount == MaxPercent)
            {
                SkinOpened?.Invoke(_id);

                if (PlayerPrefs.HasKey(FillAmountKey))
                    PlayerPrefs.DeleteKey(FillAmountKey);
            }

            yield return null;
        }
    }
}
