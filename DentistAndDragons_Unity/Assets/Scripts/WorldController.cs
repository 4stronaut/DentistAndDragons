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
    public float openCloseTimer = 25f;
    public WinEffectsontroller winEffect;
    public GameObject sign;

    private void Start()
    {
        startGame();
    }

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
        if (ts.Seconds > openCloseTimer)
        {
            startTime = DateTime.UtcNow;
            bobbert.switchMouthState();
        }
        if(false) winGame();
    }

    
    public void winGame()
    {
        winEffect.startEffects();
        bobbert.closeMouth();
        bobbert.hurt();
        bobbert.hurt();
        bobbert.hurt();
        bobbert.hurt();
        bobbert.hurt();
        bobbert.hurt();
    }

    public void resetGame()
    {
        
    }

}
