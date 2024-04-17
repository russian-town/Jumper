using System;
using Sourse.PauseContent;
using Sourse.Player.Common.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sourse.UI
{
    public class PlayerInput : MonoBehaviour, IPointerDownHandler, IPauseHandler
    {
        private PlayerAnimator _animator;
        private bool _isPause;

        public event Action Pressed;

        public void Initialize(PlayerAnimator animator)
            => _animator = animator;

        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed?.Invoke();

            if (_animator == null)
                return;

            if (_isPause == true)
                return;

            if (_animator.IsJumped == false && _animator.IsGrounded == true)
            {
                _animator.Jump();
                return;
            }

            if (_animator.IsDoubleJumped == false && _animator.IsGrounded == false)
                _animator.DoubleJump();
        }

        public void SetPause(bool isPause)
            => _isPause = isPause;
    }
}
