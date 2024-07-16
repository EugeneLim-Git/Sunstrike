using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenuStorage : MonoBehaviour
{
    [SerializeField] private List<BaseSkill> skillList;
    [SerializeField] private BaseCharacter characterStored;

    public void OnButtonClick()
    {
        // change character name, description, available skills
        // make character slot be filled with this character on click
        // have button that removes most recent entry into character comp list
    }

}
