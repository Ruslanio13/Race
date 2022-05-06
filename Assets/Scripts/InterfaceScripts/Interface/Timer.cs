using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer
{
    public float TimeLeft { get; private set; }
    public float DeltaTimePassed { get; private set; }
    public bool TimerExpired { get; set; }

    public Timer(float timeLeft)
    {
        Set(timeLeft);
    }



    public void Update(float deltaTime)
    {
        if (!GameStateManager._instance.IsGamePaused)
            Tick(deltaTime);
    }
    private void Tick(float deltaTime)
    {
        TimeLeft -= deltaTime;
        DeltaTimePassed = deltaTime;
        if (TimeLeft <= 0f)
            TimerExpired = true;
    }
    public void Set(float timeLeft)
    {
        TimerExpired = false;
        TimeLeft = timeLeft;
    }
}
