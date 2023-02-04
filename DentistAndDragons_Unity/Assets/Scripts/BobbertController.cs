using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbertController : MonoBehaviour
{
    public Animator animator;

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
    }
    
    public void closeMouth()
    {
        animator.SetInteger("AnimationState",2);
        animator.SetTrigger("AnimationTrigger");
    }

    public void hurt()
    {
        animator.SetInteger("AnimationState",3);
        animator.SetTrigger("AnimationTrigger");
    }

}
