using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    public class PlayerSpawner
    {
        public PlayerInitializer GetPlayer(PlayerInitializer template,
            PlayerPosition start,
            Vector3 targetRotation)
        {
            return Object.Instantiate(template, start.Position, Quaternion.Euler(targetRotation));
        }
    }
}
