using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Skills/Heal Skill", order = 3)]
public class HealSkill : BaseSkill
{
    public override float GetHealAmount(BattleEntity healer, BattleEntity healTarget, float classMultiplier, float genericMultiplier)
    {
        float baseHealAmount = skillValue;
        float finalHealAmount;

        finalHealAmount = Mathf.Round(((baseHealAmount * genericMultiplier) * classMultiplier) * 100.0f) * 0.1f;
        return finalHealAmount;

    }
}
