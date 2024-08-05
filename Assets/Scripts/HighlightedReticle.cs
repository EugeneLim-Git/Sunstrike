using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightedReticle : MonoBehaviour
{
    public Animator reticleAnimator;
    public SpriteRenderer reticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTargetHighlighted()
    {
        reticle.enabled = true;
        reticleAnimator.Play("HighlightStartAnimation");
    }
    public void StopHighlighting()
    {
        reticleAnimator.SetBool("toLoop", false);
        reticle.enabled = false;
    }
}
