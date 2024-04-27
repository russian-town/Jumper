using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerJumper : MonoBehaviour
    {
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpLength;
        [SerializeField] private float _doubleJumpForce;
        [SerializeField] private float _doubleJumpLength;

        private Rigidbody _rigidbody;
        private PlayerAnimator _animator;

        private void OnDestroy()
        {
            _animator.DoubleJumped -= OnDoubleJumped;
        }

        public void Initialize(PlayerAnimator animator)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = animator;
            _animator.DoubleJumped += OnDoubleJumped;
        }

        private void OnDoubleJumped()
        {
            Vector3 direction = new Vector3(_doubleJumpLength, _doubleJumpForce, 0f);
            _rigidbody.AddForce(direction);
        }
    }
}
