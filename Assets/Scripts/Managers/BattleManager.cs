using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BattleManager : MonoBehaviour
{
    private SystemManager systemManager;
    private BaseSkill currentSkill;

    [Header("Battle Data")]
    public List<BattleEntity> entityList;
    public List<BattleEntity> hostileEntityList;
    bool isActionListEmpty;
    public IEnumerator battleCoroutine;
    public List<BattleAction> actionList;
    public GameObject damageNumberPrefab;
    public GameObject healNumberPrefab;

    public void Initialise()
    {
        systemManager = FindObjectOfType<SystemManager>();
        hostileEntityList = systemManager.GetEnemyList();
        actionList = new List<BattleAction>();
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
                    Debug.Log(cubeHit.collider.GetComponent<BattleEntity>().GetCurrentHealth());
                    systemManager.SetHighlightedEnemy(cubeHit.collider.GetComponent<BattleEntity>());
                }
            }
            
        }
    }

    // To Implement:
    // every action, add to the entity list variable
    // after all actions are selected, sort by the speed values of the characters
    // and then run them all again until the entity list is empty

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
    }

    public void AddToActionList(BattleEntity characterToAdd, BattleEntity targetOfSkill, BaseSkill skillToAdd)
    {
        BattleAction battleActionToAdd = new BattleAction
        {
            character = characterToAdd,
            skillTarget = targetOfSkill,
            skillToUse = skillToAdd,
            characterSpeed = characterToAdd.GetSpeed()
        };

        actionList.Add(battleActionToAdd);
    }

    public void RunAction(BattleAction action)
    {
        action.character.OnUseSkill();
        if (action.skillToUse.GetSkillType() == BaseSkill.SkillType.Damage)
        {
            float damageDealt = action.skillToUse.GetAttackDamage(action.character, action.skillTarget, action.character.GetClassMultiplier(), 1);

            if (action.skillToUse.GetTargetRange() == BaseSkill.SkillTargetRange.All)
            {
                if (action.character.gameObject.tag == "Player")
                {
                    foreach (var enemy in systemManager.GetEnemyList())
                    {
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
                        if (player.isEntityDead() == false)
                        {
                            player.TakeDamage(damageDealt, damageNumberPrefab);
                        }
                    }
                }
            }
            else //means it's single target
            {
                action.skillTarget.TakeDamage(damageDealt, damageNumberPrefab);
                Debug.Log(action.skillTarget + " was hit for " + damageDealt + " damage!");
            }

        }
        else if (action.skillToUse.GetSkillType() == BaseSkill.SkillType.Heal)
        {
            float healAmount = action.skillToUse.GetHealAmount(action.character, action.skillTarget, action.character.GetClassMultiplier(), 1);
            action.skillTarget.RestoreHealth(healAmount, healNumberPrefab);
        }
        else if (action.skillToUse.GetSkillType() == BaseSkill.SkillType.Buff || action.skillToUse.GetSkillType() == BaseSkill.SkillType.Debuff)
        {
            Debug.Log("Debuffing!");
            float buffTotal = action.skillToUse.GetBuffAmount(action.character, action.skillTarget, action.character.GetClassMultiplier(), 1);
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

        if (action.skillTarget.GetCurrentHealth() <= 0)
        {
            action.skillTarget.setEntityDeathStatus(true);
            action.skillTarget.GetComponentInChildren<SpriteRenderer>().enabled = false;
            //action.skillTarget.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }


    public IEnumerator RunCombat()
    {
        actionList = actionList.OrderByDescending(action => action.characterSpeed).ToList();

        while (actionList.Count > 0)
        {
            yield return new WaitForSeconds(1f);
            if (actionList.Count <= 0)
            {


            }
            else if (actionList[0].character.isEntityDead() == false)
            {
                systemManager.SetHighlightedEnemy(actionList[0].skillTarget);
                systemManager.CreateActionTab(actionList[0].character.GetEntityName(), actionList[0].skillToUse.GetSkillName());
                yield return new WaitForSeconds(0.5f);
                RunAction(actionList[0]);
                
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
        yield break;

    }

    public BaseSkill GetCurrentSkill()
    {
        return currentSkill;
    }
}
