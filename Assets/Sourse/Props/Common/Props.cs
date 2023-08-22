using UnityEngine;
using UnityEngine.Events;

public class Props : MonoBehaviour
{
    public CollisionEvent CollisionEnter;
    public CollisionEvent CollisionExit;

    [SerializeField] private PlayerPosition _playerPosition;

    public PlayerPosition PlayerPosition => _playerPosition;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
            if (player.IsStart)
                CollisionEnter.Invoke();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
            if (player.IsStart)
                CollisionExit.Invoke();
    }
}

[System.Serializable]
public class CollisionEvent : UnityEvent
{
}
