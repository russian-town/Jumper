using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Player GetPlayer(Player template, PlayerPosition playerPosition)
    {
        return Instantiate(template, playerPosition.transform.position, Quaternion.identity);
    }
}
