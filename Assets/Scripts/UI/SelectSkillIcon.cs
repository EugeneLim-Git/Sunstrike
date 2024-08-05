using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkillIcon : MonoBehaviour
{
    public BaseSkill storedSkill;
    public Image background;
    private DescriptionText descText;
    [HideInInspector] public CharacterManager manager;
    public TextMeshProUGUI buttonText;

    public void Awake()
    {
        manager = FindObjectOfType<CharacterManager>();
        descText = FindObjectOfType<DescriptionText>();
    }

    public void ChangeStoredSkill(BaseSkill skillToChangeTo)
    {
        storedSkill = skillToChangeTo;
        buttonText.text = storedSkill.GetSkillName();
    }


    public void OnSelectSkill()
    {
        SelectSkillIcon button = this;
        manager = FindObjectOfType<CharacterManager>();
        manager.OnSkillSelected(storedSkill, button);
        descText.ChangeText(storedSkill.GetSkillDescription());
    }

}
