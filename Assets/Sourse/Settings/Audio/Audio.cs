using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioView), typeof(Saver))]
public class Audio : MonoBehaviour
{
    private const string SoundVolume = "SoundVolume";
    private const string MusicVolume = "MusicVolume";
    private const string SoundVolumeKey = "SoundVolumeKey";
    private const string MusicVolumeKey = "MusicVolumeKey";
    private const float MuteVolume = 0.0001f;
    private const float AudioMixerValueRatio = 20f;

    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;
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
    }

    private void OnDisable()
    {
        _soundSlider.onValueChanged.RemoveListener(ChangeSoundVolume);
        _musicSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
    }

    private void Start()
        => Initialize();

    public void ChangeSoundVolume(float value)
        => ChangeVolume(value, _soundGroup, SoundVolume, SoundVolumeKey);

    public void ChangeMusicVolume(float value)
        => ChangeVolume(value, _musicGroup, MusicVolume, MusicVolumeKey);

    private void Initialize()
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

    private void ChangeVolume(float value, AudioMixerGroup audioMixerGroup, string groupName, string key)
    {
        audioMixerGroup.audioMixer.SetFloat(groupName, Mathf.Log10(value) * AudioMixerValueRatio);
        _saver.Save(key, value);

        if (value == MuteVolume)
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
