using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Ground : MonoBehaviour
{
    [SerializeField] private AudioClip _playerFallSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
            _audioSource.PlayOneShot(_playerFallSound);
    }
}
