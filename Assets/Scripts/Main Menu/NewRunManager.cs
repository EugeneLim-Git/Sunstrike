using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewRunManager : MonoBehaviour
{
    public List<SelectSkillIcon> skillButtons;
    public List<PartySelectButton> PartyButtons;
    public DescriptionText descText;

    public CharacterManager manager;

    public void Start()
    {
        manager = FindObjectOfType<CharacterManager>();
        descText = FindObjectOfType<DescriptionText>();
        descText.ChangeText("Select up to Four characters. Select Four skills on each character.");
        foreach (PartySelectButton button in PartyButtons)
        {
            button.characterManager = manager;
        }
        foreach (SelectSkillIcon button in skillButtons)
        {
            button.manager = manager;
            button.transform.parent.gameObject.SetActive(false);
        }

        manager.Initialise();
        manager.RenewButtons();

    }

}
