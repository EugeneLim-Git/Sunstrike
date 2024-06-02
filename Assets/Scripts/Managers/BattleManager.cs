using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private SystemManager systemManager;
    private BaseSkill currentSkill;

    public void Initialise()
    {
        systemManager = FindObjectOfType<SystemManager>();
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
                if (cubeHit.collider.gameObject.CompareTag("Player") && currentSkill.GetSkillType() == BaseSkill.SkillType.Damage)
                { 
                    Debug.Log("We hit " + cubeHit.collider.name);
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
}
