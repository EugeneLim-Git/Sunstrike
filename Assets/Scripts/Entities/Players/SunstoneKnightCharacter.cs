using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunstoneKnightCharacter : BattleEntity
{
    [Header("Character Information")]
    [SerializeField] protected SunstoneKnight sunstoneKnightSO;



    // Start is called before the first frame update
    public override void Initialise()
    {
        characterName = sunstoneKnightSO.GetCharacterName();
        characterMaxHealth = sunstoneKnightSO.GetBaseHealth();
        characterCurrentHealth = characterMaxHealth;
        characterPhysicalStrength = sunstoneKnightSO.GetBasePhysicalStrength();
        characterPhysicalDefense = sunstoneKnightSO.GetBasePhysicalDefense();
        characterMagicalStrength = sunstoneKnightSO.GetBaseMagicalStrength();
        characterMagicalDefense = sunstoneKnightSO.GetBaseMagicalDefense();
        characterSpeed = sunstoneKnightSO.GetBaseSpeed();
        characterClassMultiplier = sunstoneKnightSO.buffMultiplier;
    }

    public override float UseSkill(BaseSkill skillToUse, BattleEntity skillUser, BattleEntity targetOfSkill, float generalMultiplier)
    {
        if (skillToUse.GetSkillType() == BaseSkill.SkillType.Damage)
        {
            return skillToUse.GetAttackDamage(skillUser, targetOfSkill, 1, generalMultiplier);
        }
        else if (skillToUse.GetSkillType() == BaseSkill.SkillType.Heal)
        {
            return skillToUse.GetHealAmount(skillUser, targetOfSkill, 1, generalMultiplier);
        }
        else if (skillToUse.GetSkillType() == BaseSkill.SkillType.Buff)
        {
            return skillToUse.GetBuffAmount(skillUser, targetOfSkill, characterClassMultiplier, generalMultiplier);
        }
        else if (skillToUse.GetSkillType() == BaseSkill.SkillType.Debuff)
        {
            return skillToUse.GetBuffAmount(skillUser, targetOfSkill, characterClassMultiplier, generalMultiplier);

        }
        else
        {
            return 0;
        }
    }
}
