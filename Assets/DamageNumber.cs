using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    public TextMeshPro damageText;
    public float damageValue;

    // Start is called before the first frame update
    public void Initialise(float damageToShow)
    {
        damageText = GetComponentInChildren<TextMeshPro>();  
        damageValue = damageToShow;
        damageText.text = "-" + damageToShow.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
