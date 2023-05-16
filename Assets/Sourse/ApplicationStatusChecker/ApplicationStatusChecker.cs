using UnityEngine;
using Agava.WebUtility;
using UnityEngine.Audio;

public class ApplicationStatusChecker : MonoBehaviour
{
    private const string MasterVolume = "MasterVolume";
    private const float FullVolume = 0f;
    private const float MuteVolume = -80f;

    [SerializeField] private AudioMixerGroup _masterGroup;

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeEvent;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeEvent;
    }

    public void OnInBackgroundChangeEvent(bool isChange)
    {
        if (isChange == true)
            _masterGroup.audioMixer.SetFloat(MasterVolume, MuteVolume);
        else
            _masterGroup.audioMixer.SetFloat(MasterVolume, FullVolume);
    }
}