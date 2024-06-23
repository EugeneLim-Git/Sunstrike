using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AIModule", order = 1)]
public class AIModule : ScriptableObject
{
    protected enum EnemyRole
    {
        Fighter,
        Support,
        Healer,
        Leader
    }

    protected enum EnemyType
    {
        Common,
        Miniboss,
        Boss
    }

    [Header("AI Data")]
    [SerializeField] protected EnemyRole enemyRole;
    [SerializeField] protected EnemyType enemyType;

    public BattleAction RunDecisionMaking(List<BattleEntity> entityList, List<BattleEntity> enemyList, List<BaseSkill> skillList, BattleEntity currentEnemy)
    {
        // depending on role, use list and skills appropriately and target people
        // for the purpose of this 'generic AI Module', simply pick at random based on skills

        BaseSkill selectedSkill;
        BattleEntity target;
        List<BattleEntity> aliveTargetList = new List<BattleEntity>();

        if (enemyRole == EnemyRole.Fighter)
        {
            selectedSkill = skillList[Random.Range(0, skillList.Count - 1)];
            aliveTargetList = GetPossibleTargets(entityList);
            target = aliveTargetList[Random.Range(0, aliveTargetList.Count - 1)];

            return CreateBattleAction(currentEnemy, target, selectedSkill, currentEnemy.GetSpeed());
        }
        else if (enemyRole == EnemyRole.Healer)
        {
            selectedSkill = skillList[Random.Range(0, skillList.Count - 1)];
            int howManyDead = 0;
            foreach (BattleEntity enemy in enemyList)
            {
                if (enemy.isEntityDead() == false)
                {
                    aliveTargetList.Add(enemy);
                }
                else
                {
                    howManyDead++;
                }
            }
            target = aliveTargetList[Random.Range(0, aliveTargetList.Count - 1)];

            return CreateBattleAction(currentEnemy, target, selectedSkill, currentEnemy.GetSpeed());
        }
        else if (enemyRole == EnemyRole.Support)
        {
            int choice = Random.Range(0, 1);
            List<BaseSkill> skillsToPickFrom = new List<BaseSkill>();
            if (choice == 0) // attacks or debuffs players
            {
                foreach (var skill in skillList)
                {
                    if (skill.GetSkillType() == BaseSkill.SkillType.Damage || skill.GetSkillType() == BaseSkill.SkillType.Debuff)
                    {
                        skillsToPickFrom.Add(skill);
                    }
                }
                selectedSkill = skillsToPickFrom[Random.Range(0, skillsToPickFrom.Count- 1)];
                aliveTargetList = GetPossibleTargets(entityList);
                target = aliveTargetList[Random.Range(0, aliveTargetList.Count - 1)];
            }
            else // means choice = 1, heals or buffs
            {
                foreach (var skill in skillList)
                {
                    if (skill.GetSkillType() == BaseSkill.SkillType.Heal || skill.GetSkillType() == BaseSkill.SkillType.Buff)
                    {
                        skillsToPickFrom.Add(skill);
                    }
                }
                selectedSkill = skillsToPickFrom[Random.Range(0, skillsToPickFrom.Count - 1)];

                aliveTargetList = GetPossibleTargets(enemyList);
                target = aliveTargetList[Random.Range(0, aliveTargetList.Count - 1)];
            }
            return CreateBattleAction(currentEnemy, target, selectedSkill, currentEnemy.GetSpeed());
        }
        else // means this is a 'leader'
        {
            selectedSkill = skillList[Random.Range(0, skillList.Count - 1)];
            foreach (BattleEntity entity in entityList)
            {
                if (entity.isEntityDead() == false)
                {
                    aliveTargetList.Add(entity);
                }
            }
            target = aliveTargetList[Random.Range(0, aliveTargetList.Count - 1)];

            return CreateBattleAction(currentEnemy, target, selectedSkill, currentEnemy.GetSpeed());
        }
    }

    public BattleAction CreateBattleAction(BattleEntity currentEnemy, BattleEntity target, BaseSkill skill, float enemySpeed)
    {
        BattleAction battleActionToAdd = new BattleAction
        {
            character = currentEnemy,
            skillTarget = target,
            skillToUse = skill,
            characterSpeed = enemySpeed
        };

        return battleActionToAdd;
    }

    public List<BattleEntity> GetPossibleTargets(List<BattleEntity> targetList)
    {
        List<BattleEntity> possibleTargets = new List<BattleEntity>();
        foreach (BattleEntity entity in targetList)
        {
            if (entity.isEntityDead() == false)
            {
                possibleTargets.Add(entity);
            }
        }

        return possibleTargets;
    }
}
