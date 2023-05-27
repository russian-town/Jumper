using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private List<Player> _players = new List<Player>();

    public Player GetPlayer(int id, PlayerPosition playerPosition)
    {
        if (playerPosition == null)
            Debug.Log("Set player position");

        return Instantiate(TrySearchPlayer(id), playerPosition.transform.position, Quaternion.identity);
    }

    private Player TrySearchPlayer(int id)
    {
        foreach (var player in _players)
        {
            if (player.ID == id)
            {
                return player;
            }
        }

        return null;
    }
}
