using System;
using System.Collections;
using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private const float MaxWeight = 1f;
        private const float MinWeight = 0f;
        private const string JumpParametr = "Jump";
        private const string DoubleJumpParametr = "DoubleJump";
        private const string IsGroundedParametr = "IsGrounded";
        private const string HardFallParametr = "HardFall";
        private const string DefeatParametr = "Defeat";

        [SerializeField] private float _animationDelay;
        [SerializeField] private HeadPosition _headPosition;

        private Animator _animator;
        private Coroutine _startResetTriggers;
        private float _weight;
        private GroundDetector _groundDetector;

        public bool IsJumped { get; private set; }
        public bool IsdDoubleJumped { get; private set; }
        public bool IsGrounded => _groundDetector.IsGrounded();

        public event Action Jumped;
        public event Action DoubleJumped;

        private void OnAnimatorIK()
        {
            _animator.SetLookAtWeight(_weight);
            _animator.SetLookAtPosition(_headPosition.Current);
        }

        private void OnDisable()
            => _groundDetector.Fell -= OnFell;

        private void Update()
           => _animator.SetBool(IsGroundedParametr, IsGrounded);

        public void Initialize(GroundDetector groundDetector)
        {
            _animator = GetComponent<Animator>();
            _groundDetector = groundDetector;
            _groundDetector.Fell += OnFell;
            _weight = MaxWeight;
        }

        public void CallJumpEvent()
            => Jumped?.Invoke();

        public void CallDoubleJumpEvent()
            => DoubleJumped?.Invoke();

        public void DisableIK()
            => _weight = MinWeight;

        public void Jump()
        {
            IsJumped = true;
            _animator.SetTrigger(JumpParametr);
            _startResetTriggers = StartCoroutine(ResetTriggers());
        }

        public void Defeat()
           => _animator.SetBool(DefeatParametr, true);

        public void DoubleJump()
        {
            IsdDoubleJumped = true;
            _animator.SetTrigger(DoubleJumpParametr);
            _startResetTriggers = StartCoroutine(ResetTriggers());
        }

        public void HardFall()
           => _animator.SetTrigger(HardFallParametr);

        private void OnFell(Collision collision)
        {
            IsJumped = false;
            IsdDoubleJumped = false;
        }

        private IEnumerator ResetTriggers()
        {
            if (_startResetTriggers != null)
                StopCoroutine(_startResetTriggers);

            yield return new WaitForSeconds(_animationDelay);
            _animator.ResetTrigger(JumpParametr);
            _animator.ResetTrigger(DoubleJumpParametr);
        }
    }
}
