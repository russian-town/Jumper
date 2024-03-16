using Sourse.Props.Common;
using UnityEngine;

namespace Sourse.Props.Tires
{
    public class Tires : BounceProps
    {
        protected override void Action(Collision collision, Player.Common.Scripts.Player player)
        {
            if (collision.rigidbody)
            {
                Vector3 normal = collision.GetContact(0).normal;
                Vector3 direction = collision.relativeVelocity;
                Vector3 reflect = Vector3.Reflect(direction.normalized, normal);
                float force = collision.relativeVelocity.magnitude;
                collision.rigidbody.AddForce(reflect * Mathf.Max(force, 0f), ForceMode.Impulse);
            }
        }
    }
}
