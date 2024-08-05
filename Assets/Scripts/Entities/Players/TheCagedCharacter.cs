using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCagedCharacter : BattleEntity
{
    [Header("Character Information")]
    [SerializeField] protected TheCaged theCagedSO;



    // Start is called before the first frame update
    public override void Initialise()
    {
        characterName = theCagedSO.GetCharacterName();
        characterMaxHealth = theCagedSO.GetBaseHealth();
        characterCurrentHealth = characterMaxHealth;
        characterPhysicalStrength = theCagedSO.GetBasePhysicalStrength();
        characterPhysicalDefense = theCagedSO.GetBasePhysicalDefense();
        characterMagicalStrength = theCagedSO.GetBaseMagicalStrength();
        characterMagicalDefense = theCagedSO.GetBaseMagicalDefense();
        characterSpeed = theCagedSO.GetBaseSpeed();
        characterClassMultiplier = theCagedSO.cagedDebuffMultiplier;
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
            return skillToUse.GetBuffAmount(skillUser, targetOfSkill, 1, generalMultiplier);
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

    public override void OnUseSkill(BaseSkill skillUsed)
    {
        if (skillUsed.GetSkillType() == BaseSkill.SkillType.Damage)
        {
            if (skillUsed.GetSkillValue() >= 8)
            {
                entityAnimator.Play("AttackSpecial");
            }
            else
            {
                entityAnimator.Play("Attack");
            }
        }
    }
}
