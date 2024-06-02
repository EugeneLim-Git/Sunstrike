using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private SystemManager systemManager;
    private BaseSkill currentSkill;

    [Header("Battle Data")]
    public List<BattleEntity> entityList;
    bool isEntityListEmpty = false;
    public IEnumerator battleCoroutine;

    public void Initialise()
    {
        systemManager = FindObjectOfType<SystemManager>();
        entityList = new List<BattleEntity>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && entityList.Count > 0)
        {
            battleCoroutine = RunTurn();
            StartCoroutine(battleCoroutine);
        }
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
                if (cubeHit.collider.gameObject.CompareTag("Enemy") && currentSkill.GetSkillType() == BaseSkill.SkillType.Damage)
                {
                    Debug.Log("We hit " + cubeHit.collider.name!);
                }
                else if (cubeHit.collider.gameObject.CompareTag("Enemy") && currentSkill.GetSkillType() == BaseSkill.SkillType.Heal)
                {
                    if (currentSkill.ReturnTargetRange() == BaseSkill.SkillTargetRange.Multiple)
                    {
                        systemManager.GetCurrentSelectedCharacter();
                    }
                    else // there should only be multiple or single heals
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

            
            if (cubeHit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log(cubeHit.collider.name);
                systemManager.ChangeSelectedPlayerCharacter(cubeHit.collider.GetComponent<BattleEntity>());
            }
            
        }
    }

    // To Implement:
    // every action, add to the entity list variable
    // after all actions are selected, sort by the speed values of the characters
    // and then run them all again until the entity list is empty

    public void AddToEntityList()
    {

    }

    public void ClearEntityList()
    {
        
    }



    public IEnumerator RunTurn()
    {      
        while (!isEntityListEmpty)
        {
            foreach (var entity in entityList.OrderByDescending(entity => entity.GetSpeed()))
            {
                entityList.Remove(entityList[0]);
                yield return new WaitForSeconds(1);
            }
        }
        yield return null;
    }
}
