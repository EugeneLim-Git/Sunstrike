using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionText : MonoBehaviour
{
    private TextMeshProUGUI descriptionText;

    // Start is called before the first frame update
    void Start()
    {
        descriptionText = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeText(string textToChangeTo)
    {
        descriptionText.text = textToChangeTo;
    }
}
