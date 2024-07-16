using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSisterCharacter : BattleEntity
{
    [Header("Character Information")]
    [SerializeField] protected BloodSister bloodSisterSO;
    


    // Start is called before the first frame update
    public override void  Initialise()
    {
        characterName = bloodSisterSO.GetCharacterName();
        characterMaxHealth = bloodSisterSO.GetBaseHealth();
        characterCurrentHealth = characterMaxHealth;
        characterPhysicalStrength = bloodSisterSO.GetBasePhysicalStrength();
        characterPhysicalDefense = bloodSisterSO.GetBasePhysicalDefense();
        characterMagicalStrength = bloodSisterSO.GetBaseMagicalStrength();
        characterMagicalDefense = bloodSisterSO.GetBaseMagicalDefense();
        characterSpeed = bloodSisterSO.GetBaseSpeed();
        characterClassMultiplier = bloodSisterSO.sacrificeMultiplier;
    }

    //public void ExecuteCommand(BaseSkill skillToUse, BattleEntity targetOfSkill)
    //{
    //    bloodSisterSO.UseSkill(skillToUse, this, targetOfSkill, 1);
    //}

    public override float UseSkill(BaseSkill skillToUse, BattleEntity skillUser, BattleEntity targetOfSkill, float generalMultiplier)
    {
        if (skillToUse.GetSkillType() == BaseSkill.SkillType.Damage)
        {
            return skillToUse.GetAttackDamage(skillUser, targetOfSkill, characterClassMultiplier, generalMultiplier);
        }
        else if (skillToUse.GetSkillType() == BaseSkill.SkillType.Heal)
        {
            return skillToUse.GetHealAmount(skillUser, targetOfSkill, characterClassMultiplier, generalMultiplier);
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

    public override void OnUseSkill(BaseSkill skillUsed)
    {
        if (characterCurrentHealth > characterMaxHealth / 10)
        {
            characterCurrentHealth -= characterMaxHealth / 10;
        }
        else
        {
            characterCurrentHealth = 1;
        }

        if (skillUsed.GetSkillType() == BaseSkill.SkillType.Damage)
        {
            entityAnimator.Play("Attack");
        }

    }
}
