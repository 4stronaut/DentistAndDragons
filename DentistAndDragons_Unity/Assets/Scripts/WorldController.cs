using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WorldController : MonoBehaviour
{
    [SerializeField] private BobbertController bobbert;
    [SerializeField] private bool started;
    [SerializeField] private DateTime startTime;
    
    public void startGame()
    {
        startTime = DateTime.UtcNow;
        started = true;
        bobbert.hurt();
        bobbert.openMouth();
    }

    private void Update()
    {
        if (!started) return;
        TimeSpan ts = DateTime.UtcNow - startTime;
        if (ts.Seconds > 20)
        {
            startTime = DateTime.UtcNow;
            bobbert.switchMouthState();
        }
    }

    public void stopGame()
    {
    }

}
