using System.Collections.Generic;
using Sourse.Player.Common.Scripts;
using UnityEngine;

namespace Sourse.Root
{
    public class PlayerPositionHandler : MonoBehaviour
    {
        private const string LastPropsIdKey = "CurrentPropsID";

        [SerializeField] private List<Props.Common.Props> _props = new List<Props.Common.Props>();

        private int _currentPropsId;
        private GroundDetector _groundDetector;

        public PlayerPosition StartPlayerPosition { get; private set; }
        public PlayerPosition LastPlayerPosition { get; private set; }

        public void Unsubscribe()
        {
            _groundDetector.Fell -= OnFell;
        }

        public void Subscribe(GroundDetector groundDetector)
        {
            _groundDetector = groundDetector;
            _groundDetector.Fell += OnFell;
        }

        public void Initialize()
        {
        }

        public PlayerPosition GetLastPosition()
        {
           /* if (_saver.TryGetValue(LastPropsIdKey, out int Id) == true)
                foreach (var props in _props)
                    if (_props.IndexOf(props) == Id)
                        return props.PlayerPosition;*/

            return null;
        }

        public void RemoveCurrentPropsID()
        {
            /*_saver.TryDeleteSaveData(LastPropsIdKey);*/
        }

        public void SaveCurrentPropsID()
        {
            /*_saver.Save(LastPropsIdKey, _currentPropsId);*/
        }

        private void OnFell(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Props.Common.Props props))
            {
                SetCurrentPropsId(props);
                LastPlayerPosition = GetLastPosition();
            }
        }

        private void SetCurrentPropsId(Props.Common.Props props)
        {
            if (_props.Count == 0)
                return;

            for (int i = 0; i < _props.Count; i++)
                if (_props[i] == props)
                    _currentPropsId = _props.IndexOf(props);
        }
    }
}
