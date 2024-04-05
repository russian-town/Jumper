using System;
using Sourse.Constants;
using Sourse.Game;
using Sourse.Player.Common.Scripts;
using Sourse.Save;
using UnityEngine;

namespace Sourse.Root
{
    public class PlayerServis : IDataReader
    {
        private readonly PlayerSpawner _playerSpawner = new();

        private int _id;
        private LastPropsSaver _lastPropsSaver;
        private PlayerInitializer _template;
        private PlayerInitializer _spawnedPlayer;
        private Vector3 _startRotation = new(0f, 90f, 0f);

        public PlayerServis(LastPropsSaver lastPropsSaver)
            => _lastPropsSaver = lastPropsSaver;

        public event Action<PlayerInitializer> PlayerSpawned;

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
            _spawnedPlayer = _playerSpawner.GetPlayer(LoadPlayerTemplate(),
                _lastPropsSaver.GetPlayerPositon().Position,
                _startRotation);
            PlayerSpawned?.Invoke(_spawnedPlayer);
        }

        private PlayerInitializer LoadPlayerTemplate()
        {
            string path = $"{PlayerParameter.PlayerPrefabsPath}{_id}";
            _template = Resources.Load<PlayerInitializer>(path);
            return _template;
        }
    }
}
