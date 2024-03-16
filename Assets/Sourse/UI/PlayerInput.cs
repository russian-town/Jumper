using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour, IPointerDownHandler
{
    private Player _player;

    public event Action Tap;

    public void Initialize(Player player) => _player = player;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_player == null)
            return;

        _player.Jump();
        Tap?.Invoke();
    }
}
