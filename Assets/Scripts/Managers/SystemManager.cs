using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public enum GameState
    {
        PAUSED,
        ACTIONSELECTION,
        TARGETTING,
        ENEMYDECISIONMAKING,
        BATTLING
    }
    [Header("Game State")]
    public GameState currentGameState;

    [Header("Game Managers")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private EntityManager entityManager;


    // Start is called before the first frame update
    void Start()
    {
        entityManager.Initialise();
        battleManager.Initialise();

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (currentGameState == GameState.PAUSED) //disallows all other states/inputs, as pausing should take priority
        {
        
        }
        else if (currentGameState == GameState.BATTLING) // disallows all other input as the decisions of the turn are playing
        {

        }
        else if (currentGameState == GameState.ENEMYDECISIONMAKING) // disallows players from making any inputs
        {

        }
        else if (currentGameState == GameState.ACTIONSELECTION) // takes priority over targetting for obvious reasons
        {
            battleManager.HighlightTargetInput();

        }
        else if (currentGameState == GameState.TARGETTING) // for targetting enemies or allies with a skill. lowest priority
        {
            battleManager.SelectTargetInput();
            battleManager.HighlightTargetInput();
        }
    }

    public void SetSkillToUse(BaseSkill skillToSetTo)
    {
        Debug.Log("Setting current skill to " + skillToSetTo.GetSkillName());
        battleManager.SetSkillToTargetWith(skillToSetTo);
        uiManager.SetCurrentSkillText(skillToSetTo);
    }

    public void ChangeSelectedPlayerCharacter(BattleEntity selectedPlayer)
    {
        uiManager.ChangeSelectedCharacter(selectedPlayer);
    }

}
