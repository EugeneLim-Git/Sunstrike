using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionTab : MonoBehaviour
{
    private Animator actionTabAnimator;
    public TextMeshProUGUI actionTabText;

    // Start is called before the first frame update
    public void Initialise(string skillUser, string skillName)
    {
        actionTabAnimator = GetComponent<Animator>();

        actionTabText.text = skillUser + " - " + skillName;
    }

    public void OnAnimationFinished()
    {
        Destroy(this.gameObject);
    }
}

    
