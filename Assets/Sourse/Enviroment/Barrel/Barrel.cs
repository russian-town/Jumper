using UnityEngine;

namespace Sourse.Enviroment.Barrel
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(AudioSource))]
    public class Barrel : MonoBehaviour
    {
        [SerializeField] private float _maxRelativeVelocity;
        [SerializeField] private AudioClip _fallSound;

        private Rigidbody _rigidbody;
        private AudioSource _audioSource;
        private CapsuleCollider _capsuleCollider;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _audioSource = GetComponent<AudioSource>();
            _capsuleCollider.enabled = false;
            _rigidbody.isKinematic = true;
        }

        public void AddPhysics()
        {
            _capsuleCollider.enabled = true;
            _rigidbody.isKinematic = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.relativeVelocity.magnitude > _maxRelativeVelocity)
                _audioSource.PlayOneShot(_fallSound);
        }
    }
}
