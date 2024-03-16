using UnityEngine;

namespace Sourse.Ground
{
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
            if (collision.transform.TryGetComponent(out Player.Common.Scripts.Player player) && (collision.collider is SphereCollider) == false)
                _audioSource.PlayOneShot(_playerFallSound);
        }
    }
}
