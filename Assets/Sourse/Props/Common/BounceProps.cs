using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Props.Common
{
    public abstract class BounceProps : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out PlayerAnimator playerAnimator))
                Action(collision, playerAnimator);
        }

        protected abstract void Action(Collision collision, PlayerAnimator playerAnimator);
    }
}
