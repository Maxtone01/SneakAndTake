using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : IPauseHandler
{
    private readonly List<IPauseHandler> _pauseHandlers = 
        new List<IPauseHandler>();

    public bool IsPaused { get; private set; }

    public void SetPause(bool isPaused)
    {
        IsPaused = isPaused;
        foreach (var handler in _pauseHandlers)
        {
            handler.SetPause(isPaused);
        }
    }

    public void Register(IPauseHandler pauseHandler)
    { 
        _pauseHandlers.Add(pauseHandler);
    }

    public void UnRegister(IPauseHandler pauseHandler)
    {
        _pauseHandlers.Remove(pauseHandler);
    }
}
