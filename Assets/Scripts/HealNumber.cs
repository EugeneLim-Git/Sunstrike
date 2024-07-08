using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealNumber : MonoBehaviour
{
    public TextMeshPro healText;
    public float healValue;

    // Start is called before the first frame update
    public void Initialise(float healingToShow)
    {
        healText = GetComponentInChildren<TextMeshPro>();
        healValue = healingToShow;
        healText.text = "+" + healingToShow.ToString();
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
