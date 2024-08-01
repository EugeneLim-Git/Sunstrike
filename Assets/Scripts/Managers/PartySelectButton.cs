using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PartySelectButton : MonoBehaviour
{
    [SerializeField] private int partyPosition;
    [SerializeField] public TextMeshProUGUI buttonText;

    private void Start()
    {
        buttonText.text = "Select Character";
    }
}
