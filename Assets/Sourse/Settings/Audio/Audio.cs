using Agava.WebUtility;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioView), typeof(Saver))]
public class Audio : MonoBehaviour
{
    private const string MasterVolume = "MasterVolume";
    private const string SoundVolume = "SoundVolume";
    private const string MusicVolume = "MusicVolume";
    private const string SoundVolumeKey = "SoundVolumeKey";
    private const string MusicVolumeKey = "MusicVolumeKey";
    private const float MuteVolume = -80f;
    private const float FullVolume = 0f;

    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private AudioMixerGroup _masterGroup;
    [SerializeField] private AudioMixerGroup _soundGroup;
    [SerializeField] private AudioMixerGroup _musicGroup;

    private AudioView _audioView;
    private Saver _saver;

    private void Awake()
    {
        _audioView = GetComponent<AudioView>();
        _saver = GetComponent<Saver>();
    }

    private void OnEnable()
    {
        _soundSlider.onValueChanged.AddListener(ChangeSoundVolume);
        _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeEvent;
    }

    private void OnDisable()
    {
        _soundSlider.onValueChanged.RemoveListener(ChangeSoundVolume);
        _musicSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeEvent;
    }

    public void Initialize()
    {
        if (_saver.TryGetValue(MusicVolumeKey, out float musicVolume))
        {
            ChangeMusicVolume(musicVolume);
            _musicSlider.value = musicVolume;
        }

        if (_saver.TryGetValue(SoundVolumeKey, out float soundVolume))
        {
            ChangeSoundVolume(soundVolume);
            _soundSlider.value = soundVolume;
        }
    }

    public void ChangeSoundVolume(float value)
    {
        ChangeVolume(value, _soundGroup, SoundVolume, SoundVolumeKey);
    }

    public void ChangeMusicVolume(float value)
    {
        ChangeVolume(value, _musicGroup, MusicVolume, MusicVolumeKey);
    }

    private void Mute()
    {
        _masterGroup.audioMixer.SetFloat(MasterVolume, MuteVolume);
    }

    private void Unmute()
    {
        _masterGroup.audioMixer.SetFloat(MasterVolume, FullVolume);
    }

    private void OnInBackgroundChangeEvent(bool isChange)
    {
        if (isChange == true)
            Mute();
        else
            Unmute();
    }

    private void ChangeVolume(float value, AudioMixerGroup audioMixerGroup, string groupName, string key)
    {
        audioMixerGroup.audioMixer.SetFloat(groupName, value);
        _saver.Save(key, value);

        if(value == MuteVolume)
        {
            if (audioMixerGroup == _soundGroup)
                _audioView.MuteSound();
            else if (audioMixerGroup == _musicGroup)
                _audioView.MuteMusic();
        }
        else
        {
            if (audioMixerGroup == _soundGroup)
                _audioView.UnmuteSound();
            else if (audioMixerGroup == _musicGroup)
                _audioView.UnmuteMusic();
        }
    }
}
