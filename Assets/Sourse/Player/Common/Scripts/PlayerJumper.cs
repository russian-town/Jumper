using System.Collections.Generic;
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
        [SerializeField] private List<Leg> _legs = new ();

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

            foreach (Leg leg in _legs)
                leg.Initialize(_rigidbody);
        }

        private void OnJumped()
        {
            foreach (Leg leg in _legs)
                leg.AddForce(_jumpForce, _jumpLength);
        }

        private void OnDoubleJumped()
        {
            Vector3 direction = new Vector3(_doubleJumpLength, _doubleJumpForce, 0f);
            _rigidbody.AddForce(direction);
        }
    }
}
