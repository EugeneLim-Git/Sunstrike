using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public TextMeshProUGUI gameText;

    public enum GameState
    {
        ACTIONSELECTION,
        TARGETTING,
        ENEMYDECISIONMAKING,
        BATTLING
    }
    [Header("Game State")]
    public GameState currentGameState;
    public bool isGamePaused = false;

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
        uiManager.Initialise();
        isGamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.BATTLING) // disallows all other input as the decisions of the turn are playing
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
            Debug.Log(entityManager.enemyList);
            battleManager.RunAI(entityManager.enemyList, entityManager.characterList);
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

        foreach (BattleEntity entity in entityManager.characterList)
        {
            entity.StopReticle();
        }
        foreach (BattleEntity enemy in entityManager.enemyList)
        {
            enemy.StopReticle();
        }

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
        else if (gameState == GameState.TARGETTING)
        {
            if (battleManager.GetCurrentSkill().GetSkillTargets() == BaseSkill.SkillTarget.Enemy)
            {
                foreach (BattleEntity enemy in entityManager.enemyList)
                {
                    if (enemy.isEntityDead() == false)
                    {
                        enemy.StartReticle();
                    }
                }
            }
            else if (battleManager.GetCurrentSkill().GetSkillTargets() == BaseSkill.SkillTarget.Friendly)
            {
                foreach (BattleEntity entity in entityManager.characterList)
                {
                    if (entity.isEntityDead() == false)
                    {
                        entity.StartReticle();
                    }
                }
            }
            else
            {
                foreach(BattleEntity entity in entityManager.characterList)
                {
                    if (entity.isEntityDead() == false)
                    {
                        entity.StartReticle();
                    }
                }
                foreach(BattleEntity enemy in entityManager.enemyList)
                {
                    if (enemy.isEntityDead() == false)
                    {
                        enemy.StartReticle();
                    }
                }
            }
        }
    }

    public void SetHighlightedEnemy(BattleEntity enemyToHighlight)
    {
        //Debug.Log(enemyToHighlight.GetEntityName());
        uiManager.SetEntityUIText(enemyToHighlight);

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

    public List<BattleEntity> GetEnemyList()
    {
        return entityManager.enemyList;
    }

    public BattleEntity GetCurrentSelectedCharacter()
    {
        return entityManager.selectedCharacter;
    }

    public void CreateActionTab(string skillUser, string skillName)
    {
        uiManager.CreateActionTabElement(skillUser, skillName);
    }

    public GameState GetGameState()
    {
        return currentGameState;
    }

}
