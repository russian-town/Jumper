using System.Collections;
using Sourse.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.StartScene
{
    public class LoadingBar : UIElement
    {
        [SerializeField] private Image _fill;

        private LoadingScreen _loadingScreen;
        private Coroutine _startFill;

        public void Initialize(LoadingScreen loadingScreen)
        {
            _fill.fillAmount = 0f;
            _loadingScreen = loadingScreen;
            _loadingScreen.ProgressChanged += OnProgressChanged;
        }

        public void Unsubscribe()
            => _loadingScreen.ProgressChanged -= OnProgressChanged;

        private void OnProgressChanged(float progress)
        {
            if (_startFill != null)
                StopCoroutine(_startFill);

            _startFill = StartCoroutine(Fill(progress));
        }

        private IEnumerator Fill(float progress)
        {
            while (_fill.fillAmount != progress)
            {
                _fill.fillAmount =
                    Mathf.MoveTowards(_fill.fillAmount, progress, Time.deltaTime);
                yield return null;
            }

            _startFill = null;
        }
    }
}
