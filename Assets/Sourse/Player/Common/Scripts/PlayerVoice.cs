using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerVoice : MonoBehaviour
{
    [SerializeField] private AudioClip _jump;
    [SerializeField] private AudioClip _doubleJump;
    [SerializeField] private AudioClip _fall;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Jump()
    {
        MakeSound(_jump);
    }

    public void DoubleJump()
    {
        MakeSound(_doubleJump);
    }

    public void Fall()
    {
        MakeSound(_fall);
    }

    private void MakeSound(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
