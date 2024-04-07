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
        {
            foreach (var skinSaveData in playerData.SkinSaveDatas)
            {
                if (skinSaveData.IsSelect == true)
                {
                    _id = skinSaveData.ID;
                    return;
                }
            }

            foreach (var openableSkinSaveData in playerData.OpenableSkinSaveDatas)
            {
                if (openableSkinSaveData.IsSelect == true)
                {
                    _id = openableSkinSaveData.ID;
                    return;
                }
            }

            _id = PlayerParameter.DefaultPlayerID;
        }

        public PlayerInitializer Get()
        {
            string path = $"{PlayerParameter.PlayerPrefabsPath}{_id}";
            return Resources.Load<PlayerInitializer>(path);
        }
    }
}
