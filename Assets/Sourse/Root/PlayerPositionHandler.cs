using System.Collections.Generic;
using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Root
{
    [RequireComponent(typeof(Saver.Saver))]
    public class PlayerPositionHandler : MonoBehaviour
    {
        private const string LastPropsIdKey = "CurrentPropsID";

        [SerializeField] private List<Props.Common.Props> _props = new List<Props.Common.Props>();
        [SerializeField] private Game.Game _game;

        private int _currentPropsId;
        private Saver.Saver _saver;

        public PlayerPosition LastPlayerPosition { get; private set; }

        public void Initialize()
        {
            _saver = GetComponent<Saver.Saver>();
        }

        public PlayerPosition GetLastPosition()
        {
            if (_saver.TryGetValue(LastPropsIdKey, out int Id) == true)
                foreach (var props in _props)
                    if (_props.IndexOf(props) == Id)
                        return props.PlayerPosition;

            return null;
        }

        public void RemoveCurrentPropsID()
        {
            _saver.TryDeleteSaveData(LastPropsIdKey);
        }

        public void SaveCurrentPropsID()
        {
            _saver.Save(LastPropsIdKey, _currentPropsId);
        }

        public void OnPlayerFell(PlayerPosition playerPosition, Props.Common.Props props)
        {
            if (_props.Count == 0)
                return;

            for (int i = 0; i < _props.Count; i++)
                if (_props[i] == props)
                    _currentPropsId = _props.IndexOf(props);

            _game.SetLastPosition(playerPosition);
        }
    }
}
