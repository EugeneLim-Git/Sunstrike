using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Characters/BloodSister", order = 2)]

public class BloodSister : BaseCharacter
{
    [Header("Special Stats")]
    public float sacrificeHealthPercentage = 10f;
    public float sacrificeMultiplier = 1.2f;

    public float UseSkill(BaseSkill skillToUse, BattleEntity skillUser, BattleEntity targetOfSkill, float generalMultiplier)
    {
        if (skillToUse.GetSkillType() == BaseSkill.SkillType.Damage)
        {
            return skillToUse.GetAttackDamage(skillUser, targetOfSkill, sacrificeMultiplier, generalMultiplier);
        }
        else if (skillToUse.GetSkillType() == BaseSkill.SkillType.Heal)
        {
            return skillToUse.GetHealAmount(skillUser, targetOfSkill, sacrificeMultiplier, generalMultiplier);
        }   
        else if (skillToUse.GetSkillType() == BaseSkill.SkillType.Buff)
        {
            return skillToUse.GetBuffAmount(skillUser, targetOfSkill, sacrificeMultiplier, generalMultiplier);
        }
        else
        {
            return 0;
        }
    }    
}
