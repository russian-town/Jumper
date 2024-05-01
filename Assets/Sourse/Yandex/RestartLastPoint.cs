using Sourse.Level;
using Sourse.Save;

namespace Sourse.Yandex
{
    public class RestartLastPoint : Reward, IDataWriter
    {
        private readonly LevelLoader _levelLoader;

        public RestartLastPoint(LevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
        }

        public override void Accept()
            => _levelLoader.Restart();

        public void Write(PlayerData playerData)
        {
        }
    }
}
