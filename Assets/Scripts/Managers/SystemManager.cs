using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public TextMeshProUGUI gameText;

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
    private int currentPlayerOrder;
    private bool runningCombat = false;


    // Start is called before the first frame update
    void Start()
    {
        
        entityManager.Initialise();
        battleManager.Initialise();
        currentPlayerOrder = 0;
        
    }

    // Update is called once per frame
    void Update()
    {


        if (currentGameState == GameState.PAUSED) //disallows all other states/inputs, as pausing should take priority
        {
        
        }
        else if (currentGameState == GameState.BATTLING) // disallows all other input as the decisions of the turn are playing
        {
            battleManager.HighlightTargetInput();
            if (runningCombat == false)
            {
                StartCoroutine(battleManager.RunCombat());
                runningCombat = true;
            }
        }
        else if (currentGameState == GameState.ENEMYDECISIONMAKING) // disallows players from making any inputs
        {
            //for debug and demo, enemies will not do anything right now
            SetGameState(GameState.BATTLING);
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

    public void NextPlayerCharacter()
    {
        currentPlayerOrder++;
        if (currentPlayerOrder >= entityManager.characterList.Count)
        {
            Debug.Log("Running Enemy AI!");
            SetGameState(GameState.ENEMYDECISIONMAKING);
        }
        else if (entityManager.characterList[currentPlayerOrder].isEntityDead() == true && currentPlayerOrder < entityManager.characterList.Count - 1) // current entity is dead, go to next
        {
            NextPlayerCharacter();
        }
        
        else if (entityManager.characterList[currentPlayerOrder].isEntityDead() == false) // current entity is not dead and the 
        {
            ChangeSelectedPlayerCharacter(entityManager.characterList[currentPlayerOrder]);
            SetGameState(GameState.ACTIONSELECTION);
        }

    }

    public void SetGameState(GameState gameState)
    {
        currentGameState = gameState;
        runningCombat = false;
        
        if (gameState == GameState.ACTIONSELECTION)
        {
            int i = 0;
            int deadAmount = 0;
            foreach (var enemy in entityManager.enemyList)
            {
                Debug.Log(entityManager.enemyList[i].isEntityDead());
                if (entityManager.enemyList[i].isEntityDead() == true)
                {
                    deadAmount++;
                }
                i++;
            }
            Debug.Log("entityManager.enemyList.Count");
            if (deadAmount >= entityManager.enemyList.Count)
            {
                // players have won
                Debug.Log("Player has won!");
                string winningText = "Player won!";
                gameText.text = winningText;
            }
        }
    }

    public void SetHighlightedEnemy(BattleEntity enemyToHighlight)
    {
        uiManager.SetEnemyUIText(enemyToHighlight);
    }

    public void ResetSelectedPlayer()
    {
        currentPlayerOrder = 0;
        ChangeSelectedPlayerCharacter(entityManager.characterList[0]);
    }

    public void ChangeSelectedPlayerCharacter(BattleEntity selectedPlayer)
    {
        uiManager.ChangeSelectedCharacter(selectedPlayer);
        entityManager.selectedCharacter = selectedPlayer;
    }

    public BaseSkill GetCurrentSelectedSkill()
    {
        return uiManager.GetCurrentSkill();
    }

    public BattleEntity GetCurrentSelectedCharacter()
    {
        return entityManager.selectedCharacter;
    }

}
