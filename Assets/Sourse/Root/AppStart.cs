using System.Collections.Generic;
using Sourse.Save;
using UnityEngine;

namespace Sourse.Root
{
    public class AppStart : MonoBehaviour, IDataReader, IDataWriter
    {
        private ISaveLoadService _saveLoadService;
        private LocalSave _localSave;

        private void Start()
            => Initialize();

        private void Initialize()
        {
            _localSave = new LocalSave
                (
                new List<IDataReader> { this },
                new List<IDataWriter> { this }
                );

            _saveLoadService = _localSave;
            _saveLoadService.Load();
        }

        public void Read(PlayerData playerData)
        {

        }

        public void Write(PlayerData playerData)
        {

        }
    }
}
