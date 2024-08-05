using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingReticle : MonoBehaviour
{
    public SpriteRenderer reticle;
    public Animator reticleAnimator;

    // Start is called before the first frame update
    void Start()
    {
        reticle.enabled = false;
        reticleAnimator.enabled = false;
        
    }

    public void SetToActive()
    {
        Debug.Log("setting reticle to active");
        reticle.enabled = true;
        reticleAnimator.enabled = true;
        reticleAnimator.Play("TargetAnimStart");
        reticleAnimator.SetBool("isActive", true);
    }

    public void SetToInactive()
    {
        reticle.enabled = false;
        reticleAnimator.enabled = false;
    }
    
}
