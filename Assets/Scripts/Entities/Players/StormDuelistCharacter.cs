using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormDuelistCharacter : BattleEntity
{
    [Header("Character Information")]
    [SerializeField] protected StormDuelist stormDuelistSO;



    // Start is called before the first frame update
    public override void Initialise()
    {
        characterName = stormDuelistSO.GetCharacterName();
        characterMaxHealth = stormDuelistSO.GetBaseHealth();
        characterCurrentHealth = characterMaxHealth;
        characterPhysicalStrength = stormDuelistSO.GetBasePhysicalStrength();
        characterPhysicalDefense = stormDuelistSO.GetBasePhysicalDefense();
        characterMagicalStrength = stormDuelistSO.GetBaseMagicalStrength();
        characterMagicalDefense = stormDuelistSO.GetBaseMagicalDefense();
        characterSpeed = stormDuelistSO.GetBaseSpeed();
        characterClassMultiplier = stormDuelistSO.stormMultiplier;
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
            return skillToUse.GetBuffAmount(skillUser, targetOfSkill, 1, generalMultiplier);
        }
        else
        {
            return 0;
        }
    }

    public override void OnUseSkill(BaseSkill skillUsed)
    {
        if (skillUsed.GetSkillValue() >= 10)
        {
            entityAnimator.Play("AttackSpecial");
        }
        else
        {
            entityAnimator.Play("Attack");
        }
    }
}
