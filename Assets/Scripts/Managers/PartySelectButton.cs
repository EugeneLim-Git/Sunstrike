using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PartySelectButton : MonoBehaviour
{
    [SerializeField] private int partyPosition;
    public CharacterManager characterManager;
    [SerializeField] public TextMeshProUGUI buttonText;

    private void Awake()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        buttonText.text = "Select Character";
    }

    public void OnPartyMemberSelected()
    {
        characterManager.SetSelectedCharacter(partyPosition);
    }
}
