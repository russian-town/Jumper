using System.Collections.Generic;
using Sourse.Enviroment.Common;
using Sourse.Player.Common.Scripts;
using Sourse.Save;
using UnityEngine;

namespace Sourse.Game
{
    public class LastPropsSaver : IDataReader, IDataWriter
    {
        private readonly List<Props> _props;
        private readonly GroundDetector _groundDetector;
        private readonly PlayerPosition _startPosition;

        private PlayerPosition _savedPosition;
        private int _savedPropsIndex;

        public LastPropsSaver(List<Props> props,
            GroundDetector groundDetector,
            PlayerPosition startPosition)
        {
            _props = props;
            _groundDetector = groundDetector;
            _startPosition = startPosition;
        }

        public int LastPropsIndex { get; private set; }

        public void Subscribe()
            => _groundDetector.Fell += OnFell;

        public void Unsubscribe()
            => _groundDetector.Fell -= OnFell;

        public void Read(PlayerData playerData)
        {
            _savedPropsIndex = playerData.SavedPropsIndex;

            if (_savedPropsIndex < 0)
                return;

            _savedPosition = _props[_savedPropsIndex].PlayerPosition;
            ClearIndex();
        }

        public void Write(PlayerData playerData)
            => playerData.SavedPropsIndex = _savedPropsIndex;

        public void SaveIndex()
            => _savedPropsIndex = LastPropsIndex;

        public void ClearIndex()
            => _savedPropsIndex = -1;

        public PlayerPosition GetPlayerPositon()
        {
            if (_savedPosition != null)
                return _savedPosition;

            return _startPosition;
        }

        private void OnFell(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Props props))
                if (_props.Contains(props))
                    LastPropsIndex = _props.IndexOf(props);
        }
    }
}
