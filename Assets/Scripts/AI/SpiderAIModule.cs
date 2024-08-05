using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AIModule", order = 1)]
public class SpiderAIModule : AIModule
{

    public override BattleAction RunDecisionMaking(List<BattleEntity> entityList, List<BattleEntity> enemyList, List<BaseSkill> skillList, BattleEntity currentEnemy)
    {
        // depending on role, use list and skills appropriately and target people
        // for the purpose of this 'generic AI Module', simply pick at random based on skills

        BaseSkill selectedSkill;
        BattleEntity target;
        List<BattleEntity> aliveTargetList = new List<BattleEntity>();
        selectedSkill = skillList[Random.Range(0, skillList.Count)];


        if (selectedSkill.GetSkillType() == BaseSkill.SkillType.Damage)
        {
            aliveTargetList = GetPossibleTargets(entityList);
            target = aliveTargetList[Random.Range(0, aliveTargetList.Count)];

            return CreateBattleAction(currentEnemy, target, selectedSkill, currentEnemy.GetSpeed());
        }
        else if (selectedSkill.GetSkillType() == BaseSkill.SkillType.Buff || selectedSkill.GetSkillType() == BaseSkill.SkillType.Debuff)
        {
            if (selectedSkill.GetSkillType() == BaseSkill.SkillType.Buff) // debuffs players
            {
                aliveTargetList = GetPossibleTargets(entityList);
                target = aliveTargetList[Random.Range(0, aliveTargetList.Count)];
            }
            else // means debuff
            {
                aliveTargetList = GetPossibleTargets(enemyList);
                target = aliveTargetList[Random.Range(0, aliveTargetList.Count)];
            }
            return CreateBattleAction(currentEnemy, target, selectedSkill, currentEnemy.GetSpeed());
        }
        else
        {
            //means heal??
            aliveTargetList = GetPossibleTargets(enemyList);
            target = aliveTargetList[Random.Range(0, aliveTargetList.Count)];
            return CreateBattleAction(currentEnemy, target, selectedSkill, currentEnemy.GetSpeed());
        }
    }
}
