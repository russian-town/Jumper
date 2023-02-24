using UnityEngine;
using UnityEngine.Events;

public class Props : MonoBehaviour
{
    public CollisionEvent CollisionEnter;
    public CollisionEvent CollisionExit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
            CollisionEnter.Invoke();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
            CollisionExit.Invoke();
    }
}

[System.Serializable]
public class CollisionEvent : UnityEvent
{
}
