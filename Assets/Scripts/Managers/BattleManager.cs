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
    

    public void Initialise()
    {
        systemManager = FindObjectOfType<SystemManager>();
        entityList = new List<BattleEntity>();
        hostileEntityList = new List<BattleEntity>();
        actionList = new List<BattleAction>();
    }

    private void Update()
    {
        
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
                if (cubeHit.collider.gameObject.CompareTag("Enemy") && currentSkill.GetSkillType() == BaseSkill.SkillType.Damage && cubeHit.collider.GetComponent<BattleEntity>().isEntityDead() == false) // this is done so we don't heal enemies
                {
                    
                    AddToActionList(systemManager.GetCurrentSelectedCharacter(), cubeHit.collider.GetComponent<BattleEntity>(), currentSkill);
                    systemManager.NextPlayerCharacter();
                }
                else if (cubeHit.collider.gameObject.CompareTag("Player") && currentSkill.GetSkillType() == BaseSkill.SkillType.Heal) // this is done because we don't want player targetting friends with damaging attacks
                {
                    AddToActionList(systemManager.GetCurrentSelectedCharacter(), cubeHit.collider.GetComponent<BattleEntity>(), currentSkill);
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

            
            if (cubeHit.collider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log(cubeHit.collider.GetComponent<BattleEntity>().GetCurrentHealth());
                systemManager.SetHighlightedEnemy(cubeHit.collider.GetComponent<BattleEntity>());
            }
            
        }
    }

    // To Implement:
    // every action, add to the entity list variable
    // after all actions are selected, sort by the speed values of the characters
    // and then run them all again until the entity list is empty

    public void RunAI(List<BattleEntity> enemyList)
    {
        foreach (var enemy in enemyList)
        {
            AIModule ai = enemy.GetAIModule();
            actionList.Add(ai.RunDecisionMaking(entityList, enemyList, enemy.skillList, enemy));
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
            if (action.skillToUse.GetSkillType() == BaseSkill.SkillType.Damage)
            {
                float damageDealt = action.skillToUse.GetAttackDamage(action.character, action.skillTarget, action.character.GetClassMultiplier(), 1);
                action.skillTarget.TakeDamage(damageDealt);
                Debug.Log(action.skillTarget + " was hit for " + damageDealt + " damage!");
                // need to implement damage calc for AoE attacks
            }
            else if (action.skillToUse.GetSkillType() == BaseSkill.SkillType.Heal)
            {
                float healAmount = action.skillToUse.GetHealAmount(action.character, action.skillTarget, action.character.GetClassMultiplier(), 1);
                action.skillTarget.RestoreHealth(healAmount);
            }
            else if (action.skillToUse.GetSkillType() == BaseSkill.SkillType.Buff)
            {
                float buffTotal = action.skillToUse.GetBuffAmount(action.character, action.skillTarget, action.character.GetClassMultiplier(), 1);
                //implement different buff targets later
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
}
