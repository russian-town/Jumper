using System.Collections;
using Lean.Localization;
using Sourse.Constants;
using Sourse.UI.Shop.SkinConfiguration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.UI.LevelCompletePanel
{
    public class LevelCompletePanel : UIElement
    {
        [SerializeField] private float _speed;
        [SerializeField] private Image _openingSkinBar;
        [SerializeField] private Image _openingSkinBarBackground;
        [SerializeField] private TMP_Text _completeLevelText;

        public void Initialize(float currentOpenedPercent, SkinConfig skinConfig)
        {
            _openingSkinBar.sprite = skinConfig.Icon;
            _openingSkinBarBackground.color = Color.black;
            _openingSkinBarBackground.sprite = skinConfig.Icon;
            _openingSkinBar.fillAmount = currentOpenedPercent;
        }

        public void SetText(int levelNumber)
        {
            string translatedLevel = LeanLocalization.GetTranslationText(TranslationText.Level);
            string translatedComplete = LeanLocalization.GetTranslationText(TranslationText.Completed);
            string translationText = $"{translatedLevel} {levelNumber} {translatedComplete}";
            _completeLevelText.text = translationText;
        }

        public void HideOpeningSkinBar()
        {
            _openingSkinBar.enabled = false;
            _openingSkinBarBackground.enabled = false;
        }

        public void StartFillSkinBarCoroutine(float targetFillAmount)
        {
            Show();
            _openingSkinBar.enabled = true;
            _openingSkinBarBackground.enabled = true;
            StartCoroutine(FillSkinBarOverTime(targetFillAmount));
        }

        private IEnumerator FillSkinBarOverTime(float targetFillAmount)
        {
            while (_openingSkinBar.fillAmount != targetFillAmount)
            {
                _openingSkinBar.fillAmount = Mathf.MoveTowards(
                    _openingSkinBar.fillAmount,
                    targetFillAmount,
                    _speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
