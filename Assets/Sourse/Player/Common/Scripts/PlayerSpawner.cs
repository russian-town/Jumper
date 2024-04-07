using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    public class PlayerSpawner
    {
        private readonly Vector3 _startRotation = new(0f, 90f, 0f);

        public PlayerInitializer Create(PlayerInitializer template, Vector3 startPosition)
        {
            return Object.Instantiate(template, startPosition, Quaternion.Euler(_startRotation));
        }
    }
}
