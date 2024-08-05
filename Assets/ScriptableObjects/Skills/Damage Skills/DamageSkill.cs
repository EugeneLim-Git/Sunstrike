using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Skills/Damage Skill", order = 2)]
public class DamageSkill : BaseSkill
{
    //Damage skills should be simple: Do damage, based on a number, multiplied by the related stat scaler.
    //Physical skills use the physical strength stat. Magical skills use the magical strength stat. They then will be affected by opposing defense stats.

    public override float GetAttackDamage(BattleEntity attacker, BattleEntity defender, float classMultiplier, float genericMultiplier)
    {
        float skillPower = skillValue * classMultiplier * genericMultiplier;
        float finalDamage;

        if (skillScalerType == SkillScaler.Physical)
        {
            float defense = defender.GetPhysicalDefense();
            float magicDefense = defender.GetMagicalDefense();
            if (defense < 1)
            {
                defense = 1;
            }
            finalDamage = skillPower * (attacker.GetPhysicalStrength() / defense);
            //Debug.Log(skillPower, attacker.GetPhysicalStrength(), defender.GetPhysicalDefense());
        }
        else
        {
            float magicDefense = defender.GetMagicalDefense();
            if (magicDefense < 1)
            {
                magicDefense = 1;
            }


            finalDamage = skillPower * (attacker.GetMagicalStrength() / magicDefense);

        }

        if (finalDamage <= 0)
        {
            finalDamage = 0.1f;
        }

        finalDamage = Mathf.Round(finalDamage * 10.0f) * 0.1f;

        return finalDamage;

        //uses a simple system that scales skill power off of the remainder of strength divided by defense
        //usually results in very simple or slight power increases or decreases. eg. if skill power is 20, strength is 12 and defense is 10, results in a damage multiplier of 1.2
        //final damage is equal to 20 * 1.2, which is 24
        // damage should be dealt by the combat manager
    }

}
