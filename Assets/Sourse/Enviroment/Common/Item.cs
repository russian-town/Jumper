using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Enviroment.Common
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private CollisionEvent CollisionEnter;
        [SerializeField] private CollisionEvent CollisionExit;

        public Vector3 Position
            => new (transform.position.x, 1f, transform.position.z);

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out PlayerInitializer _))
                    CollisionEnter.Invoke();
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.transform.TryGetComponent(out PlayerInitializer _))
                    CollisionExit.Invoke();
        }
    }
}
