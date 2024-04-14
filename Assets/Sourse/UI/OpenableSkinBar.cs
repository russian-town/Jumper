using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.UI
{
    public class OpenableSkinBar : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _fillArea;
        [SerializeField] private float _speed;

        public void Initialize(Sprite icon, float currentValue)
        {
            _background.color = Color.black;
            _background.sprite = icon;
            _fillArea.sprite = icon;
            _fillArea.fillAmount = currentValue;
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

        public void StartFillSkinBarCoroutine(float targetFillAmount)
        {
            if (enabled == false || gameObject.activeInHierarchy == false)
                return;

            StartCoroutine(FillSkinBarOverTime(targetFillAmount));
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
