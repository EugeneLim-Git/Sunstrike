using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Skills/BuffDebuff Skill", order = 1)]
public class BuffDebuffSkill : BaseSkill
{
    //Damage skills should be simple: Do damage, based on a number, multiplied by the related stat scaler.
    //Physical skills use the physical strength stat. Magical skills use the magical strength stat. They then will be affected by opposing defense stats.

    public override float GetBuffAmount(BattleEntity attacker, BattleEntity defender, float classMultiplier, float genericMultiplier)
    {
        float multiplierValue = skillValue;
        float finalMultiplierValue;

        finalMultiplierValue = (skillValue * classMultiplier) * genericMultiplier;
        return finalMultiplierValue;
    }

}
