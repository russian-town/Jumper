using System.Collections;
using Sourse.Game.FinishContent;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.UI
{
    public class OpenableSkinBar : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _fillArea;
        [SerializeField] private float _speed;
        
        private OpenableSkinViewFiller _openableSkinViewFiller;

        public void Initialize(Sprite icon,
            float currentValue,
            OpenableSkinViewFiller openableSkinViewFiller)
        {
            _background.color = Color.black;
            _background.sprite = icon;
            _fillArea.sprite = icon;
            _fillArea.fillAmount = currentValue;
            _openableSkinViewFiller = openableSkinViewFiller;
            _openableSkinViewFiller.Initialized += OnInitialized;
            _openableSkinViewFiller.PercentCalculated += OnPercentCalculated;
            Hide();
        }

        private void OnInitialized(Sprite sprite, float percent)
        {
            _background.sprite = sprite;
            _fillArea.fillAmount = percent;
        }

        private void OnPercentCalculated(float persent)
        {
            Show();
            StartCoroutine(FillSkinBarOverTime(persent));
        }

        public void Show()
        {
            _fillArea.enabled = true;
            _background.enabled = true;
        }

        public void Hide()
        {
            _fillArea.enabled = false;
            _background.enabled = false;
        }

        public void Unsubscribe()
        {
            _openableSkinViewFiller.Initialized -= OnInitialized;
            _openableSkinViewFiller.PercentCalculated -= OnPercentCalculated;
        }

        private IEnumerator FillSkinBarOverTime(float targetFillAmount)
        {
            while (_fillArea.fillAmount != targetFillAmount)
            {
                _fillArea.fillAmount = Mathf.MoveTowards(
                    _fillArea.fillAmount,
                    targetFillAmount,
                    _speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
