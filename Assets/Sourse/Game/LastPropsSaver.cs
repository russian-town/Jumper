using System.Collections.Generic;
using Sourse.Enviroment.Common;
using Sourse.Player.Common.Scripts;
using Sourse.Save;
using UnityEngine;

namespace Sourse.Game
{
    public class LastPropsSaver : IDataWriter
    {
        private List<Props> _props;
        private GroundDetector _groundDetector;
        private int _lastPropsIndex;

        public LastPropsSaver(List<Props> props, GroundDetector groundDetector)
        {
            _props = props;
            _groundDetector = groundDetector;
            SetDefaultIndex();
        }

        public void Subscribe()
            => _groundDetector.Fell += OnFell;

        public void Unsubscribe()
            => _groundDetector.Fell -= OnFell;

        public void Write(PlayerData playerData)
        {
            playerData.LastPropsIndex = _lastPropsIndex;
        }

        private void OnFell(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Props props))
                SetLastIndex(props);
        }

        private void SetLastIndex(Props props)
        {
            if (_props.Contains(props))
                _lastPropsIndex = _props.IndexOf(props);
        }

        private void SetDefaultIndex()
            => _lastPropsIndex = -1;
    }
}
