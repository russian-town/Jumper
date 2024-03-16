using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sourse.UI
{
    public class PlayerInput : MonoBehaviour, IPointerDownHandler
    {
        private Player.Common.Scripts.Player _player;

        public event Action Pressed;

        public void Initialize(Player.Common.Scripts.Player player) => _player = player;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_player == null)
                return;

            _player.Jump();
            Pressed?.Invoke();
        }
    }
}
