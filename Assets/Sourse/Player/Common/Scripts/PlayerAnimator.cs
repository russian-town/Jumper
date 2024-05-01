using System;
using Sourse.Constants;
using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private GroundDetector _groundDetector;
        private int _jumpCount = 0;

        public event Action Jumped;

        public event Action DoubleJumped;

        public void Subscribe()
        {
            _groundDetector.PlayerJumped += OnPlayerJumped;
            _groundDetector.PlayerFell += OnPlayerFell;
        }

        public void Unsubscribe()
        {
            _groundDetector.PlayerJumped -= OnPlayerJumped;
            _groundDetector.PlayerFell -= OnPlayerFell;
        }

        public void Initialize(GroundDetector groundDetector)
        {
            _animator = GetComponent<Animator>();
            _groundDetector = groundDetector;
            _animator.SetBool(AnimationName.IsGrounded, _groundDetector.IsGrounded);
        }

        public void CallJumpEvent()
            => Jumped?.Invoke();

        public void CallDoubleJumpEvent()
            => DoubleJumped?.Invoke();

        public void Jump()
        {
            if (_jumpCount == 2)
                return;

            _jumpCount++;
            _animator.SetInteger(AnimationName.JumpCount, _jumpCount);
        }

        private void HardFall()
           => _animator.SetTrigger(AnimationName.HardFall);

        private void Defeat()
           => _animator.SetBool(AnimationName.Defeat, true);

        private void Die(float relativeVelocity)
        {
            if (relativeVelocity >= AnimationParameter.MaxRelativeVelocityY)
                HardFall();
            else
                Defeat();
        }

        private void OnPlayerJumped()
        {
            _animator.SetBool(AnimationName.IsGrounded, _groundDetector.IsGrounded);
        }

        private void OnPlayerFell()
        {
            _animator.SetBool(AnimationName.IsGrounded, _groundDetector.IsGrounded);
            ResetJumping();
        }

        private void ResetJumping()
        {
            _jumpCount = 0;
            _animator.SetInteger(AnimationName.JumpCount, _jumpCount);
        }
    }
}
