using System.Collections.Generic;
using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private List<PlayerInitializer> _players = new List<PlayerInitializer>();

        public PlayerInitializer GetPlayer(int id, PlayerPosition playerPosition)
        {
            if (TrySearchPlayer(id, out PlayerInitializer player))
                return Instantiate(player, playerPosition.transform.position, Quaternion.identity);

            return null;
        }

        private bool TrySearchPlayer(int id, out PlayerInitializer searchPlayer)
        {
            foreach (var player in _players)
            {
                if (player.ID == id)
                {
                    searchPlayer = player;
                    return true;
                }
            }

            searchPlayer = null;
            return false;
        }
    }
}
