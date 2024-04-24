using System;
using Sourse.Enviroment.Common;

namespace Sourse.Game.FinishContent
{
    public class Finish : Item
    {
        public event Action LevelCompleted;

        public void CompleteLevel()
        {
            LevelCompleted?.Invoke();
        }
    }
}
