using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    public class PlayerSpawner
    {
        public PlayerInitializer GetPlayer(PlayerInitializer template)
        {
            return Object.Instantiate(template);
        }
    }
}
