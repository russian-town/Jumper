using System.Collections.Generic;
using UnityEngine;

public class PolicecarLight : MonoBehaviour
{
    [SerializeField] private List<Light> _lights = new List<Light>();
    [SerializeField] private float _maxIntensity;
    [SerializeField] private float _lightSpeed;

    private bool _isTurnOn;
    private float _minIntensity = 0f;

    private void Update()
    {
        if (_isTurnOn == false)
            return;

        TurnOnFlashers();
    }

    public void ChangeFlashersState(bool isTurnOn)
    {
        _isTurnOn = isTurnOn;

        if (isTurnOn == false)
            SwitchOffFlashers();
    }

    private void TurnOnFlashers()
    {
        foreach (var light in _lights)
        {
            light.intensity = Mathf.PingPong(Time.time * _lightSpeed, _maxIntensity);
        }
    }

    private void SwitchOffFlashers()
    {
        foreach (var light in _lights)
        {
            light.intensity = _minIntensity;
        }
    }
}
