using System.Collections.Generic;
using Sourse.Constants;
using UnityEngine;

namespace Sourse.Save
{
    public class LocalSave : ISaveLoadService
    {
        private List<IDataReader> _dataReaders;
        private List<IDataWriter> _dataWriters;
        private PlayerData _playerData = new PlayerData();

        public LocalSave(List<IDataReader> dataReaders,
            List<IDataWriter> dataWriters)
        {
            _dataReaders = dataReaders;
            _dataWriters = dataWriters;
        }

        public void Save(PlayerData playerData)
        {
            foreach (var dataWriter in _dataWriters)
                dataWriter.Write(_playerData);

            string saveData = JsonUtility.ToJson(playerData);
            PlayerPrefs.SetString(SaveKeys.PlayerProgress, saveData);
            PlayerPrefs.Save();
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(SaveKeys.PlayerProgress) == false)
                return;

            string data = PlayerPrefs.GetString(SaveKeys.PlayerProgress);

            if (string.IsNullOrEmpty(data))
                return;

            _playerData = JsonUtility.FromJson<PlayerData>(data);

            if (_playerData == null)
                _playerData = new PlayerData();

            foreach (var dataReader in _dataReaders)
                dataReader.Read(_playerData);
        }
    }
}
