using System;
using Sourse.Enviroment.Common;

namespace Sourse.Ground
{
    public class DeadZone : Item
    {
        public event Action LevelLost;

        public void GameOver()
        {
            LevelLost?.Invoke();
        }
    }
}
