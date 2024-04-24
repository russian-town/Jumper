using Sourse.Constants;
using Sourse.Player.Common.Scripts;
using Sourse.Save;
using UnityEngine;

namespace Sourse.Root
{
    public class PlayerTemplateLoader : IDataReader
    {
        private int _id;

        public void Read(PlayerData playerData)
            => _id = playerData.CurrentSelectedSkinID;

        public PlayerInitializer Get()
        {
            string path = $"{PersistentPath.PlayerPrefabs}{_id}";
            return Resources.Load<PlayerInitializer>(path);
        }
    }
}
