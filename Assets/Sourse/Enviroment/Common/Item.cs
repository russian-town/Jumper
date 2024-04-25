using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Enviroment.Common
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] private CollisionEvent<Vector3> CollisionEnter;
        [SerializeField] private CollisionEvent CollisionExit;

        public Vector3 Position => transform.position;

        public virtual void Initialize() { }

        private void OnCollisionEnter(Collision collision)
        {
            if (CheckCollision(collision, out Vector3 point))
                CollisionEnter.Invoke(point);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.transform.TryGetComponent(out PlayerInitializer _))
                CollisionExit.Invoke();
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
