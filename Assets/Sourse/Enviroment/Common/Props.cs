using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Enviroment.Common
{
    public class Props : MonoBehaviour
    {
        [SerializeField] private CollisionEvent CollisionEnter;
        [SerializeField] private CollisionEvent CollisionExit;
        [SerializeField] private PlayerPosition _playerPosition;

        public PlayerPosition PlayerPosition => _playerPosition;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Player.Common.Scripts.PlayerInitializer player))
            {
                if (player.IsStart)
                {
                    CollisionEnter.Invoke();
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Player.Common.Scripts.PlayerInitializer player))
            {
                if (player.IsStart)
                {
                    CollisionExit.Invoke();
                }
            }
        }
    }
}
