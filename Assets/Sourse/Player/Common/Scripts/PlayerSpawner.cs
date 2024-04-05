using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    public class PlayerSpawner
    {
        public PlayerInitializer GetPlayer(PlayerInitializer template,
            Vector3 startPosition,
            Vector3 startRotation)
        {
            return Object.Instantiate(template, startPosition, Quaternion.Euler(startRotation));
        }
    }
}
