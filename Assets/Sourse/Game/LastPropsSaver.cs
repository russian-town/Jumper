using System.Collections.Generic;
using Sourse.Enviroment.Common;
using Sourse.Player.Common.Scripts;
using Sourse.Save;
using UnityEngine;

namespace Sourse.Game
{
    public class LastPropsSaver : IDataReader, IDataWriter
    {
        private List<Props> _props;
        private GroundDetector _groundDetector;
        private Props _lastProps;
        private int _lastPropsIndex;
        private PlayerPosition _startPosition;
        private PlayerPosition _savedPosition;

        public LastPropsSaver(List<Props> props,
            GroundDetector groundDetector,
            PlayerPosition startPosition)
        {
            _props = props;
            _groundDetector = groundDetector;
            _startPosition = startPosition;
        }

        public void Subscribe()
            => _groundDetector.Fell += OnFell;

        public void Unsubscribe()
            => _groundDetector.Fell -= OnFell;

        public void Read(PlayerData playerData)
        {
            _lastPropsIndex = playerData.LastPropsIndex;

            if (_lastPropsIndex > 0)
                _savedPosition = _props[_lastPropsIndex].PlayerPosition;
        }

        public void Write(PlayerData playerData)
        {
            Debug.Log(_lastPropsIndex);
            playerData.LastPropsIndex = _lastPropsIndex;
        }

        public void SetLastIndex()
        {
            if (_props.Contains(_lastProps))
                _lastPropsIndex = _props.IndexOf(_lastProps);
        }

        public void ClearLastIndex()
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
            {
                _lastProps = props;
                Debug.Log(_lastProps.name);
            }
        }
    }
}
