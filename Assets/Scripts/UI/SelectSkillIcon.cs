using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSkillIcon : MonoBehaviour
{
    private BaseSkill storedSkill;

    public void ChangeStoredSkill(BaseSkill skillToChangeTo)
    {
        storedSkill = skillToChangeTo;
    }

    public void OnSelectSkill(CharacterManager manager)
    {
        manager.OnSkillSelected(storedSkill);
    }
}
