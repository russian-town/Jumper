using Sourse.Enviroment.Common;
using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Enviroment.Tires
{
    public class Tires : BounceProps
    {
        protected override void Action(Collision collision, PlayerAnimator playerAnimator)
            => playerAnimator.Jump();
    }
}
