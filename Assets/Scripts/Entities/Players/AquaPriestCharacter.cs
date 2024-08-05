using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaPriestCharacter : BattleEntity
{
    [Header("Character Information")]
    [SerializeField] protected AquaPriest aquaPriestSO;



    // Start is called before the first frame update
    public override void Initialise()
    {
        characterName = aquaPriestSO.GetCharacterName();
        characterMaxHealth = aquaPriestSO.GetBaseHealth();
        characterCurrentHealth = characterMaxHealth;
        characterPhysicalStrength = aquaPriestSO.GetBasePhysicalStrength();
        characterPhysicalDefense = aquaPriestSO.GetBasePhysicalDefense();
        characterMagicalStrength = aquaPriestSO.GetBaseMagicalStrength();
        characterMagicalDefense = aquaPriestSO.GetBaseMagicalDefense();
        characterSpeed = aquaPriestSO.GetBaseSpeed();
        characterClassMultiplier = aquaPriestSO.healMultiplier;
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
        if (skillUsed.GetSkillType() == BaseSkill.SkillType.Damage)
        {
            entityAnimator.Play("Attack");
        }
        else
        {
            entityAnimator.Play("Heal");
        }

    }
}
