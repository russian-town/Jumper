using UnityEngine;

public abstract class BounceProps : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            Action(collision, player);
        }
    }

    protected abstract void Action(Collision collision, Player player);
}
