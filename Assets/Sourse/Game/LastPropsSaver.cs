using System;
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
        private int _lastPropsIndex;

        public LastPropsSaver(List<Props> props,
            GroundDetector groundDetector,
            PlayerPosition startPosition)
        {
            _props = props;
            _groundDetector = groundDetector;
            _startPosition = startPosition;
        }

        public event Action<int> IndexSaved;

        public void Subscribe()
            => _groundDetector.Fell += OnFell;

        public void Unsubscribe()
            => _groundDetector.Fell -= OnFell;

        public void Read(PlayerData playerData)
        {
            _lastPropsIndex = playerData.LastPropsIndex;

            if (_lastPropsIndex < 0)
                return;

            _savedPosition = _props[_lastPropsIndex].PlayerPosition;
            ClearIndex();
        }

        public void Write(PlayerData playerData)
        {
            playerData.LastPropsIndex = _lastPropsIndex;
            IndexSaved?.Invoke(_lastPropsIndex);
        }

        public void ClearIndex()
            => _lastPropsIndex = -1;

        public PlayerPosition GetPlayerPositon()
        {
            if(_savedPosition != null)
                return _savedPosition;

            return _startPosition;
        }

        private void OnFell(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Props props))
                if (_props.Contains(props))
                    _lastPropsIndex = _props.IndexOf(props);
        }
    }
}
