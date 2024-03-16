using System;
using System.Collections;
using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.UI.LevelCompletePanel
{
    public class LevelCompletePanel : UIElement
    {
        private const string FillAmountKey = "FillAmount";
        private const float MaxPercent = 1f;

        [SerializeField] private float _speed;
        [SerializeField] private Image _openingSkinBar;
        [SerializeField] private Image _openingSkinBarBackground;
        [SerializeField] private TMP_Text _completeLevelText;

        private int _id;

        public event Action<int> SkinOpened;

        public void Initialize(Skin skin)
        {
            _openingSkinBar.sprite = skin.Icon;
            _openingSkinBarBackground.color = Color.black;
            _openingSkinBarBackground.sprite = skin.Icon;
            _id = skin.ID;
        }

        public void SetText(int levelNumber)
        {
            string translationText = $"{LeanLocalization.GetTranslationText("LEVEL")} {levelNumber} {LeanLocalization.GetTranslationText("COMPLETE")}";
            _completeLevelText.text = translationText;
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
}
