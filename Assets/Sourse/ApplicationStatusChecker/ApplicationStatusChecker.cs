using Agava.WebUtility;
using Sourse.Constants;
using Sourse.Pause;
using Sourse.Yandex;
using UnityEngine;
using UnityEngine.Audio;

namespace Sourse.ApplicationStatusChecker
{
    public class ApplicationStatusChecker : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _masterGroup;
        [SerializeField] private Pause.Pause _pause;
        [SerializeField] private PauseView _pausePanel;

        private bool _isPlayInterstitial = false;
        private bool _isPlayRewarded = false;
        private YandexAds _yandexAds;

        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeEvent;
        }

        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeEvent;

            if (_yandexAds == null)
                return;

            _yandexAds.OpenInterstitialCallback -= OnOpenInterstitial;
            _yandexAds.CloseInterstitialCallback -= OnCloseInterstiital;
        }

        private void OnApplicationFocus(bool focus)
        {
            OnInBackgroundChangeEvent(!focus);

            if (focus == false)
            {
                if (_pause == null)
                    return;

                _pause.Enable();
                _pausePanel.Show();
            }
        }

        public void Initialize(YandexAds yandexAds)
        {
            _yandexAds = yandexAds;
            _yandexAds.OpenInterstitialCallback += OnOpenInterstitial;
            _yandexAds.CloseInterstitialCallback += OnCloseInterstiital;
        }

        public void OnInBackgroundChangeEvent(bool isChange)
        {
            if (_isPlayInterstitial || _isPlayRewarded)
                return;

            ChangeSoundStatus(isChange);
        }

        public void ChangeSoundStatus(bool isMute)
        {
            if (isMute == true)
                _masterGroup.audioMixer.SetFloat(AudioParameters.MasterGroup,
                    AudioParameters.MuteVolume);
            else
                _masterGroup.audioMixer.SetFloat(AudioParameters.MasterGroup,
                    AudioParameters.FullVolume);
        }

        public void SetIsPlayRewarded(bool isPlayRewaeded)
        {
            _isPlayRewarded = isPlayRewaeded;
        }

        private void OnOpenInterstitial()
        {
            _isPlayInterstitial = true;
        }

        private void OnCloseInterstiital(bool isClose)
        {
            _isPlayInterstitial = false;
        }
    }
}