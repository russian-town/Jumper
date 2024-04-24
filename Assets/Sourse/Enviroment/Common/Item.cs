using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Enviroment.Common
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private CollisionEvent<Vector3> CollisionEnter;
        [SerializeField] private CollisionEvent<Vector3> CollisionExit;

        public Vector3 Position
            => new (transform.position.x, 1f, transform.position.z);

        private void OnCollisionEnter(Collision collision)
        {
            if (CheckCollision(collision, out Vector3 point))
                CollisionEnter.Invoke(point);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (CheckCollision(collision, out Vector3 point))
                CollisionExit.Invoke(point);
        }

        private bool CheckCollision(Collision collision, out Vector3 point)
        {
            if (collision.transform.TryGetComponent(out PlayerInitializer _))
            {
                ContactPoint contact = collision.contacts[0];
                point = contact.point;
                return true;
            }

            point = Vector3.zero;
            return false;
        }
    }
}
