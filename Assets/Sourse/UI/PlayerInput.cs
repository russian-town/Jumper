using System;
using Sourse.PauseContent;
using Sourse.Player.Common.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sourse.UI
{
    public class PlayerInput : MonoBehaviour, IPointerDownHandler, IPauseHandler
    {
        private readonly bool _isGameOver;
        private readonly bool _isLevelComleted;

        private PlayerAnimator _animator;
        private bool _isPause;

        public event Action Pressed;

        public void Initialize(PlayerAnimator animator)
            => _animator = animator;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isPause)
                return;

            _animator.Jump();
            Pressed?.Invoke();
        }

        public void SetPause(bool isPause)
            => _isPause = isPause;
    }
}
