using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    public class PlayerSpawner
    {
        public PlayerInitializer GetPlayer(PlayerInitializer template, PlayerPosition playerPosition)
        {
            return Object.Instantiate(template, playerPosition.Position, Quaternion.identity);
        }
    }
}
