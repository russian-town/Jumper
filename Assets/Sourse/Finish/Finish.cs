using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Finish : MonoBehaviour
{
    [SerializeField] private AudioClip _fallSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.SetPause(true);
            _audioSource.PlayOneShot(_fallSound);
        }
    }
}
