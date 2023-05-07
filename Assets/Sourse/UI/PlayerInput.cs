using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour, IPointerDownHandler
{
    public event UnityAction Tap;

    private Player _player;

    public void Initialize(Player player)
    {
        _player = player;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_player == null)
            return;

        _player.Jump();
        Tap?.Invoke();
    }
}
