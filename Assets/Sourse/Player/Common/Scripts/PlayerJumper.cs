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
        [SerializeField] private float _radius;
        [SerializeField] private float _maxDistance;
        [SerializeField] private LayerMask _propsLayer;
        [SerializeField] private Transform _startRayPoint;

        private Rigidbody _rigidbody;
        private PlayerAnimator _animator;

        private void OnDestroy()
        {
            _animator.Jumped -= OnJumped;
            _animator.DoubleJumped -= OnDoubleJumped;
        }

        public void Initialize(PlayerAnimator animator)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = animator;
            _animator.Jumped += OnJumped;
            _animator.DoubleJumped += OnDoubleJumped;
        }

        private void OnJumped()
        {
            Ray ray = new Ray(_startRayPoint.position, -_startRayPoint.up);

            if (Physics.SphereCast(ray, _radius, out RaycastHit hitInfo, _maxDistance, _propsLayer))
            {
                _rigidbody.AddForce(hitInfo.normal * _jumpForce);
                _rigidbody.AddForce(Vector3.right * _jumpLength);
            }
        }

        private void OnDoubleJumped()
        {
            Vector3 direction = new Vector3(_doubleJumpLength, _doubleJumpForce, 0f);
            _rigidbody.AddForce(direction);
        }
    }
}
