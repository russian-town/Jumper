using UnityEngine;

namespace Sourse.Finish
{
    [RequireComponent(typeof(AudioSource))]
    public class LevelCompleteSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip _fallSound;

        private AudioSource _audioSource;

        private void Awake() => _audioSource = GetComponent<AudioSource>();

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Player.Common.Scripts.PlayerInitializer player))
                _audioSource.PlayOneShot(_fallSound);
        }
    }
}
