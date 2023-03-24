using UnityEngine;
using UnityEngine.Events;

public class Props : MonoBehaviour
{
    public event UnityAction<PlayerPosition> PlayerFell;
    public CollisionEvent CollisionEnter;
    public CollisionEvent CollisionExit;

    [SerializeField] private PlayerPosition _playerPosition;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            CollisionEnter.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
            CollisionExit.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Player player))
            PlayerFell?.Invoke(_playerPosition);
    }
}

[System.Serializable]
public class CollisionEvent : UnityEvent
{
}
