using UnityEditor.PackageManager;
using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class PhysicsJump : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _jumpDuration;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private AnimationCurve _doubleJumpDuration;
        [SerializeField] private float _doubleJumpHeight;
        [SerializeField] private AnimationCurve _doubleJumpCurve;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private SurfaceSlider _surfaceSlider;

        private JumpAnimation _jumpAnimation;
        private PureAnimation _pureAnimation;
        private PlayerAnimator _playerAnimator;
        private float _jumpLenght;
        private float _doubleJumpLenght;
        private BoxCollider _boxCollider;
        private PureAnimation _currentAnimation;
        private Vector3 _target;
        private Vector3 _halfSize;
        private RaycastHit _hitInfo;

        public void Subsctibe()
        {
            _playerAnimator.Jumped += OnJumped;
            _playerAnimator.DoubleJumped += OnDoubleJumped;
        }

        public void Unsubscribe()
        {
            _playerAnimator.Jumped -= OnJumped;
            _playerAnimator.DoubleJumped -= OnDoubleJumped;
        }

        public void Initialize(
            JumpAnimation jumpAnimation,
            PlayerAnimator playerAnimator,
            float jumpLenght,
            float doubleJumpLenght)
        {
            _jumpAnimation = jumpAnimation;
            _playerAnimator = playerAnimator;
            _pureAnimation = new PureAnimation(this);
            _jumpLenght = jumpLenght;
            _doubleJumpLenght = doubleJumpLenght;
            _boxCollider = GetComponent<BoxCollider>();
            _halfSize = _boxCollider.size / 2;
        }

        private void OnJumped()
            => Jump(transform.forward, _jumpDuration, _jumpLenght, _jumpHeight, _jumpCurve);

        private void OnDoubleJumped()
            => Jump(transform.forward, _doubleJumpDuration, _doubleJumpLenght, _doubleJumpHeight, _doubleJumpCurve);

        private void Jump(Vector3 direction, AnimationCurve duration, float lenght, float height, AnimationCurve animationCurve)
        {
            _pureAnimation?.Stop();
            _currentAnimation?.Stop();
            Quaternion orientation = transform.localRotation;
            Vector3 startPosition = transform.position;
            _target = transform.position + (direction * lenght);

            if (Physics.BoxCast(
                _target,
                _halfSize,
                Vector3.down,
                out _hitInfo,
                orientation,
                Mathf.Infinity,
                _groundMask))
            {
                _target = new Vector3(_target.x, _hitInfo.point.y + _halfSize.y, _target.z) +
                    _surfaceSlider.Project(transform.forward.normalized, _hitInfo.normal);
            }

            _currentAnimation = _jumpAnimation.Play(transform, duration, height, animationCurve);

            _pureAnimation.Play(duration, (progress) =>
            {
                transform.position = Vector3.Lerp(startPosition, _target, progress) + _currentAnimation.LastChanges.Positon;
                return null;
            });
        }

        private void OnDrawGizmos()
        {
            if(_boxCollider == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_target, _boxCollider.size);
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(_hitInfo.point, new Vector3(_target.x, _hitInfo.point.y + _halfSize.y, _target.z) +
                    _surfaceSlider.Project(transform.forward.normalized, _hitInfo.point));
        }
    }
}
