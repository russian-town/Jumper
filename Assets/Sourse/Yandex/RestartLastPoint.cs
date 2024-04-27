using Sourse.Level;
using Sourse.Player.Common.Scripts;
using Sourse.Save;

namespace Sourse.Yandex
{
    public class RestartLastPoint : Reward, IDataWriter
    {
        private readonly LevelLoader _levelLoader;

        private PlayerPosition _lastPosition;

        public RestartLastPoint(LevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
        }

        public override void Accept()
            => _levelLoader.Restart();

        public void Write(PlayerData playerData)
            => playerData.SpawnPosition = _lastPosition.Position;
    }
}
