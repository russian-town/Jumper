using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Saver))]
public class PlayerPositionHandler : MonoBehaviour
{
    private const string LastPropsIdKey = "CurrentPropsID";

    [SerializeField] private List<Props> _props = new List<Props>();
    [SerializeField] private Game _game;

    private int _currentPropsId;
    private Saver _saver;

    public PlayerPosition LastPlayerPosition { get; private set; }

    public void Initialize()
    {
        _saver = GetComponent<Saver>();
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

    public void OnPlayerFell(PlayerPosition playerPosition, Props props)
    {
        if (_props.Count == 0)
            return;

        for (int i = 0; i < _props.Count; i++)
            if (_props[i] == props)
                _currentPropsId = _props.IndexOf(props);

        _game.SetLastPosition(playerPosition);
    }
}
