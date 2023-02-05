using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.PoseDetection.Debug;
using UnityEngine;
using UnityEngine.Serialization;

public class BobbertController : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public bool mouthOpen;
    
    private void Awake()
    {
        animator.GetComponent<Animator>();
    }

    public void idle()
    {
        animator.SetInteger("AnimationState",0);
        animator.SetTrigger("AnimationTrigger");
    } 
    
    public void openMouth()
    {
        animator.SetInteger("AnimationState",1);
        animator.SetTrigger("AnimationTrigger");
        mouthOpen = true;
    }
    
    public void closeMouth()
    {
        animator.SetInteger("AnimationState",2);
        animator.SetTrigger("AnimationTrigger");
        mouthOpen = false;
    }

    public void hurt()
    {
        audioSource.Play();
        animator.SetInteger("AnimationState",3);
        animator.SetTrigger("AnimationTrigger");
    }

    public void switchMouthState()
    {
        mouthOpen = !mouthOpen;
        if (mouthOpen)
        {
            closeMouth();
        }
        else
        {
            openMouth();
        }
    }
}
