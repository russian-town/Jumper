using UnityEngine;

namespace Sourse.Props.Common
{
    public abstract class BounceProps : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Player.Common.Scripts.Player player))
            {
                Action(collision, player);
            }
        }

        protected abstract void Action(Collision collision, Player.Common.Scripts.Player player);
    }
}
