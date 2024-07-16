using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static System.Collections.Specialized.BitVector32;
using static UnityEngine.EventSystems.EventTrigger;

public class BattleManager : MonoBehaviour
{
    private SystemManager systemManager;
    private BaseSkill currentSkill;

    [Header("Battle Data")]
    public List<BattleEntity> entityList;
    public List<BattleEntity> hostileEntityList;
    public IEnumerator battleCoroutine;
    public List<BattleAction> actionList;
    public GameObject damageNumberPrefab;
    public GameObject healNumberPrefab;
    private int roundsPassed;

    public void Initialise()
    {
        systemManager = FindObjectOfType<SystemManager>();
        hostileEntityList = systemManager.GetEnemyList();
        actionList = new List<BattleAction>();
        roundsPassed = 1;
    }

    public void SetSkillToTargetWith(BaseSkill skillToUse)
    {
        currentSkill = skillToUse;
    }    

    public void SelectTargetInput() // to be called in System Manager when an input is required for 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 cubeRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D cubeHit = Physics2D.Raycast(cubeRay, Vector2.zero);
            

            if (systemManager.currentGameState == SystemManager.GameState.TARGETTING)
            {
                if (cubeHit.collider == true && currentSkill != null)
                {
                    if (cubeHit.collider.GetComponent<BattleEntity>().isEntityDead() == false) // this is done so we don't heal enemies
                    {
                        if (cubeHit.collider.gameObject.CompareTag("Player")) // this is done because we don't want player targetting friends with damaging attacks
                        {
                            if (currentSkill.GetSkillType() == BaseSkill.SkillType.Heal || currentSkill.GetSkillType() == BaseSkill.SkillType.Buff)
                            {
                                AddToActionList(systemManager.GetCurrentSelectedCharacter(), cubeHit.collider.GetComponent<BattleEntity>(), currentSkill);
                                systemManager.NextPlayerCharacter();
                            }
                            else
                            {

                            }
                        }
                        else if (cubeHit.collider.gameObject.CompareTag("Enemy"))
                        {
                            if (currentSkill.GetSkillType() == BaseSkill.SkillType.Damage || currentSkill.GetSkillType() == BaseSkill.SkillType.Debuff)
                            {
                                AddToActionList(systemManager.GetCurrentSelectedCharacter(), cubeHit.collider.GetComponent<BattleEntity>(), currentSkill);
                                systemManager.NextPlayerCharacter();
                            }
                            else
                            {

                            }
                        }
                    }
                    else
                    {
                        // nothing happens
                    }
                }
            }
        }
    }

    public void HighlightTargetInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 cubeRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D cubeHit = Physics2D.Raycast(cubeRay, Vector2.zero);

            if (cubeHit.collider == true)
            {
                if (cubeHit.collider.GetComponent<BattleEntity>() == true)
                {
                    if (cubeHit.collider.GetComponent<BattleEntity>().isEntityDead() == false)
                    {
                        //Debug.Log(cubeHit.collider.GetComponent<BattleEntity>().GetCurrentHealth());
                        systemManager.SetHighlightedEnemy(cubeHit.collider.GetComponent<BattleEntity>());
                    }
                }
            }
            
        }
    }

    public void RunAI(List<BattleEntity> enemyList, List<BattleEntity> playerList)
    {
        foreach (BattleEntity enemy in enemyList)
        {
            if (enemy.isEntityDead() != true)
            {
                AIModule ai = enemy.GetAIModule();
                Debug.Log(entityList.Count);
                actionList.Add(ai.RunDecisionMaking(playerList, enemyList, enemy.skillList, enemy));
            }
        }
        systemManager.SetGameState(SystemManager.GameState.BATTLING);
    }

    public void AddToActionList(BattleEntity characterToAdd, BattleEntity targetOfSkill, BaseSkill skillToAdd)
    {
        BattleAction battleActionToAdd = new BattleAction
        {
            character = characterToAdd,
            skillTarget = targetOfSkill,
            skillToUse = skillToAdd,
            characterSpeed = characterToAdd.GetSpeed() + skillToAdd.GetSkillSpeedMod()
        };

        actionList.Add(battleActionToAdd);
    }

    public IEnumerator RunAction(BattleAction action)
    {
        action.character.OnUseSkill(action.skillToUse);
        yield return new WaitForSeconds(action.skillToUse.GetSkillAnimDuration());
        if (action.skillToUse.GetSkillType() == BaseSkill.SkillType.Damage)
        {
            if (action.skillToUse.GetTargetRange() == BaseSkill.SkillTargetRange.All)
            {
                if (action.character.gameObject.tag == "Player")
                {
                    foreach (var enemy in systemManager.GetEnemyList())
                    {
                        float damageDealt = action.character.UseSkill(action.skillToUse, action.character, action.skillTarget, 1);
                        if (enemy.isEntityDead() == false)
                        {
                            enemy.TakeDamage(damageDealt, damageNumberPrefab);
                        }
                    }
                }
                else if (action.character.gameObject.tag == "Enemy")
                {
                    foreach (var player in entityList)
                    {
                        float damageDealt = action.character.UseSkill(action.skillToUse, action.character, action.skillTarget, 1);
                        if (player.isEntityDead() == false)
                        {
                            player.TakeDamage(damageDealt, damageNumberPrefab);
                        }
                    }
                }
            }
            else //means it's single target
            {
                float damageDealt = action.character.UseSkill(action.skillToUse, action.character, action.skillTarget, 1);
                action.skillTarget.TakeDamage(damageDealt, damageNumberPrefab);
                Debug.Log(action.skillTarget + " was hit for " + damageDealt + " damage!");
            }

        }
        else if (action.skillToUse.GetSkillType() == BaseSkill.SkillType.Heal)
        {
            float healAmount = action.character.UseSkill(action.skillToUse, action.character, action.skillTarget, 1);
            action.skillTarget.RestoreHealth(healAmount, healNumberPrefab);
        }
        else if (action.skillToUse.GetSkillType() == BaseSkill.SkillType.Buff || action.skillToUse.GetSkillType() == BaseSkill.SkillType.Debuff)
        {
            //Debug.Log("Debuffing!");
            //float buffTotal = action.skillToUse.GetBuffAmount(action.character, action.skillTarget, action.character.GetClassMultiplier(), 1);
            float buffTotal = action.character.UseSkill(action.skillToUse, action.character, action.skillTarget, 1);
            BaseSkill.SkillScaler statToMod = action.skillToUse.GetSkillScalerType();

            if (statToMod == BaseSkill.SkillScaler.Physical)
            {
                action.skillTarget.AddToPhysicalStrengthMod(buffTotal);
            }
            else if (statToMod == BaseSkill.SkillScaler.Magical)
            {
                action.skillTarget.AddToMagicalStrengthMod(buffTotal);
            }
            else if (statToMod == BaseSkill.SkillScaler.MagicalDefense)
            {
                action.skillTarget.AddToMagicalDefenseMod(buffTotal);
            }
            else if (statToMod == BaseSkill.SkillScaler.PhysicalDefense)
            {
                action.skillTarget.AddToPhysicalDefenseMod(buffTotal);
            }
            else //means the skill scales the speed mod stat
            {
                action.skillTarget.AddToSpeedMod(buffTotal);
            }
        }

        Debug.Log(action.skillToUse.GetSecondaryEffect());
        if (action.skillToUse.GetSecondaryEffect())
        {
            Debug.Log("I have a second effect!");
            BaseSkill secondEffect = action.skillToUse.GetSecondaryEffect();

            if (secondEffect.GetSkillTargets() == BaseSkill.SkillTarget.Self)
            {
                ModifyTargetStats(secondEffect.GetSkillScalerType(), action.character, secondEffect.GetBuffAmount(action.character, action.character, action.character.GetClassMultiplier(), 1));
            }
            else if (secondEffect.GetSkillTargets() == BaseSkill.SkillTarget.Friendly)
            {
                ModifyTargetStats(secondEffect.GetSkillScalerType(), action.character, secondEffect.GetBuffAmount(action.character, action.skillTarget, action.character.GetClassMultiplier(), 1));
            }
            else if (secondEffect.GetSkillTargets() == BaseSkill.SkillTarget.Enemy) // targets enemies only
            {
                ModifyTargetStats(secondEffect.GetSkillScalerType(), action.skillTarget, secondEffect.GetBuffAmount(action.character, action.skillTarget, action.character.GetClassMultiplier(), 1));
            }
        }


        if (action.skillTarget.GetCurrentHealth() <= 0)
        {
            action.skillTarget.setEntityDeathStatus(true);
            //action.skillTarget.GetComponentInChildren<SpriteRenderer>().enabled = false;
            //action.skillTarget.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        systemManager.SetHighlightedEnemy(actionList[0].skillTarget);
    }


    public IEnumerator RunCombat()
    {
        actionList = actionList.OrderByDescending(action => action.character.GetSpeed()).ToList();

        while (actionList.Count > 0)
        {
            if (actionList[0].character.isEntityDead() == false)
            {
                yield return new WaitForSeconds(1f);
            }
            if (actionList.Count <= 0)
            {


            }
            else if (actionList[0].character.isEntityDead() == false)
            {
                systemManager.SetHighlightedEnemy(actionList[0].skillTarget);
                systemManager.CreateActionTab(actionList[0].character.GetEntityName(), actionList[0].skillToUse.GetSkillName());
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(RunAction(actionList[0]));
                yield return new WaitForSeconds(0.5f);

                Destroy(actionList[0]);
                actionList.Remove(actionList[0]);
                
                Debug.Log(actionList.Count);

            }
            else
            {
                Destroy(actionList[0]);
                actionList.Remove(actionList[0]);
                Debug.Log(actionList.Count);
            }
            
        }
        systemManager.SetGameState(SystemManager.GameState.ACTIONSELECTION);
        systemManager.ResetSelectedPlayer();
        roundsPassed++;
        yield return new WaitForSeconds(1.0f);
        systemManager.CreateActionTabRounds();
        yield break;

    }

    public int GetRoundsPassed()
    {
        return roundsPassed;
    }

    public BaseSkill GetCurrentSkill()
    {
        return currentSkill;
    }

    public void ModifyTargetStats(BaseSkill.SkillScaler statToMod, BattleEntity target, float buffTotal)
    {
        if (statToMod == BaseSkill.SkillScaler.Physical)
        {
            target.AddToPhysicalStrengthMod(buffTotal);
        }
        else if (statToMod == BaseSkill.SkillScaler.Magical)
        {
            target.AddToMagicalStrengthMod(buffTotal);
        }
        else if (statToMod == BaseSkill.SkillScaler.MagicalDefense)
        {
            target.AddToMagicalDefenseMod(buffTotal);
        }
        else if (statToMod == BaseSkill.SkillScaler.PhysicalDefense)
        {
            target.AddToPhysicalDefenseMod(buffTotal);
        }
        else //means the skill scales the speed mod stat
        {
            target.AddToSpeedMod(buffTotal);
        }
    }
}
