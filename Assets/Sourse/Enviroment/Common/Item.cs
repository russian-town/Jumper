using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Enviroment.Common
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private CollisionEvent CollisionEnter;
        [SerializeField] private CollisionEvent CollisionExit;

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
