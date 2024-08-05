using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistSpiderBoss : BattleEntity
{
    [Header("Character Information")]
    [SerializeField] protected BaseCharacter battleEntitySO;

    // Start is called before the first frame update
    public override void Initialise()
    {
        characterName = battleEntitySO.GetCharacterName();
        characterMaxHealth = battleEntitySO.GetBaseHealth();
        characterCurrentHealth = characterMaxHealth;
        characterPhysicalStrength = battleEntitySO.GetBasePhysicalStrength();
        characterPhysicalDefense = battleEntitySO.GetBasePhysicalDefense();
        characterMagicalStrength = battleEntitySO.GetBaseMagicalStrength();
        characterMagicalDefense = battleEntitySO.GetBaseMagicalDefense();
        characterSpeed = battleEntitySO.GetBaseSpeed();
        characterClassMultiplier = battleEntitySO.GetClassMultiplier();
    }

    public override void OnUseSkill(BaseSkill skillUsed)
    {
        if (skillUsed.GetSkillType() == BaseSkill.SkillType.Damage)
        {
            if (skillUsed.GetSkillScalerType() == BaseSkill.SkillScaler.Physical)
            {
                if (skillUsed.GetTargetRange() == BaseSkill.SkillTargetRange.All)
                {
                    entityAnimator.Play("AttackSpecial");
                }
                else
                {
                    entityAnimator.Play("Attack");
                }
            }
            else
            {
                entityAnimator.Play("MagicAttack");
            }
        }
        else if (skillUsed.GetSkillType() == BaseSkill.SkillType.Buff || skillUsed.GetSkillType() == BaseSkill.SkillType.Debuff)
        {
            entityAnimator.Play("Buff");
        }
    }
}
