using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();

    public void Initialize(IPauseHandler[] pauseHandlers)
    {
        _pauseHandlers.AddRange(pauseHandlers);
    }

    public void Enable()
    {
        foreach (var pauseHandler in _pauseHandlers)
        {
            pauseHandler.SetPause(true);
        }
    }

    public void Disable()
    {
        foreach (var pauseHandler in _pauseHandlers)
        {
            pauseHandler.SetPause(false);
        }
    }
}
