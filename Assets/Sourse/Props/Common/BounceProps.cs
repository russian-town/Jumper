using UnityEngine;

public abstract class BounceProps : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.Bounce();
            Action();
        }
    }

    protected abstract void Action();
}
