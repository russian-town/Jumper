using System;
using System.Collections;
using Lean.Localization;
using Sourse.UI.Shop.SkinConfiguration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.UI.LevelCompletePanel
{
    public class LevelCompletePanel : UIElement
    {
        private const string FillAmountKey = "FillAmount";
        private const string Level = "LEVEL";
        private const string Complete = "COMPLETE";
        private const float MaxPercent = 1f;

        [SerializeField] private float _speed;
        [SerializeField] private Image _openingSkinBar;
        [SerializeField] private Image _openingSkinBarBackground;
        [SerializeField] private TMP_Text _completeLevelText;

        private int _id;

        public float CurrentFillAmount => _openingSkinBar.fillAmount;

        public event Action<int> SkinOpened;

        public void Initialize(SkinConfig skin)
        {
            _openingSkinBar.sprite = skin.Icon;
            _openingSkinBarBackground.color = Color.black;
            _openingSkinBarBackground.sprite = skin.Icon;
            _id = skin.ID;
        }

        public void SetText(int levelNumber)
        {
            string translatedLevel = LeanLocalization.GetTranslationText(Level);
            string translatedComplete = LeanLocalization.GetTranslationText(Complete);
            string translationText = $"{translatedLevel} {levelNumber} {translatedComplete}";
            _completeLevelText.text = translationText;
        }

        public void HideOpeningSkinBar()
        {
            _openingSkinBar.enabled = false;
            _openingSkinBarBackground.enabled = false;
        }

        public void StartFillSkinBarCoroutine(float targetFillAmount)
            => StartCoroutine(FillSkinBarOverTime(targetFillAmount));

        private IEnumerator FillSkinBarOverTime(float targetFillAmount)
        {
            while (_openingSkinBar.fillAmount != targetFillAmount)
            {
                _openingSkinBar.fillAmount = Mathf.MoveTowards(
                    _openingSkinBar.fillAmount,
                    targetFillAmount,
                    _speed * Time.deltaTime);
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
