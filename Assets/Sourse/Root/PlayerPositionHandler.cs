using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Saver))]
public class PlayerPositionHandler : MonoBehaviour
{
    private const string LastPropsIDKey = "CurrentPropsID";

    [SerializeField] private List<Props> _props = new List<Props>();
    [SerializeField] private Game _game;

    private int _currentPropsID;
    private Saver _saver;

    public PlayerPosition LastPlayerPosition { get; private set; }

    public void Initialize()
    {
        _saver = GetComponent<Saver>();
    }

    public PlayerPosition GetLastPosition()
    {
        if (_saver.TryGetValue(LastPropsIDKey, out int ID) == true)
        {
            foreach (var props in _props)
            {
                if (_props.IndexOf(props) == ID)
                {
                    return props.PlayerPosition;
                }
            }
        }

        return null;
    }

    public void RemoveCurrentPropsID()
    {
        _saver.TryDeleteSaveData(LastPropsIDKey);
    }

    public void SaveCurrentPropsID()
    {
        _saver.Save(LastPropsIDKey, _currentPropsID);
    }

    public void OnPlayerFell(PlayerPosition playerPosition, Props props)
    {
        if (_props.Count == 0)
            return;

        for (int i = 0; i < _props.Count; i++)
        {
            if(_props[i] == props)
            {
                _currentPropsID = _props.IndexOf(props);
            }
        }

        _game.SetLastPosition(playerPosition);
    }
}
