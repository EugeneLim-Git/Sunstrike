using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public TextMeshProUGUI gameText;
    private BattleEntity currentHighlightedTarget;

    public enum GameState
    {
        ACTIONSELECTION,
        TARGETTING,
        ENEMYDECISIONMAKING,
        BATTLING,
        PLAYERWON
    }
    [Header("Game State")]
    public GameState currentGameState;
    public bool isGamePaused = false;

    [Header("Game Managers")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private EntityManager entityManager;
    [SerializeField] private AudioManager audioManager;
    public CharacterManager characterManager;

    private int currentPlayerOrder;
    private bool runningCombat = false;

    // Start is called before the first frame update
    void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();   
        entityManager.Initialise();
        battleManager.Initialise();

        currentPlayerOrder = 0;
        audioManager.Initialise();
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

        }
        else if (currentGameState == GameState.ACTIONSELECTION && isGamePaused == false) // takes priority over targetting for obvious reasons
        {
            battleManager.HighlightTargetInput();
        }
        else if (currentGameState == GameState.TARGETTING && isGamePaused == false) // for targetting enemies or allies with a skill. lowest priority
        {
            battleManager.SelectTargetInput();
            battleManager.HighlightTargetInput();
        }
        else if (currentGameState == GameState.PLAYERWON)
        {

        }
    }

    public void SetSkillToUse(BaseSkill skillToSetTo)
    {
        Debug.Log("Setting current skill to " + skillToSetTo.GetSkillName());
        battleManager.SetSkillToTargetWith(skillToSetTo);
        uiManager.SetCurrentSkillText(skillToSetTo);
    }

    public void CreateActionTabRounds()
    {
        uiManager.CreateActionTabElement("Round", "" + battleManager.GetRoundsPassed());
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
        else
        {
            Debug.Log("Running Enemy AI!");
            SetGameState(GameState.ENEMYDECISIONMAKING);
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
                if (entityManager.encounterNumber >= 3)
                {
                    gameText.text = "Player has won! End of Combat Beta.";
                    SetGameState(GameState.PLAYERWON);
                }
                entityManager.GetNewEnemyList();
                ResetHighlightedEntity();
                //SetHighlightedEnemy(entityManager.enemyList[0]);

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
                        Debug.Log("Hii");
                        entity.StartReticle();
                    }
                }
            }
            else if (battleManager.GetCurrentSkill().GetSkillTargets() == BaseSkill.SkillTarget.Self)
            {
                entityManager.selectedCharacter.StartReticle();
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
        else if (gameState == GameState.ENEMYDECISIONMAKING)
        {
            battleManager.RunAI(entityManager.enemyList, entityManager.characterList);
        }
    }

    public void SetHighlightedEnemy(BattleEntity enemyToHighlight)
    {
        if (uiManager.highlightedTarget == null)
        {
            uiManager.highlightedTarget = enemyToHighlight;
            Debug.Log("highlighting for 1st time");
        }
        else // means that there was a previous highlighted target
        {
            if (uiManager.highlightedTarget != uiManager.selectedPlayerCharacter)
            {
                uiManager.highlightedTarget.StopHighlighting();
                uiManager.highlightedTarget = enemyToHighlight;
            }
        }

        //Debug.Log(enemyToHighlight.GetEntityName());
        uiManager.SetEntityUIText(enemyToHighlight);

    }

    public List<BattleEntity> GetEntityList()
    {
        return entityManager.GetEntityList();
    }
    public void ResetSelectedPlayer()
    {
        currentPlayerOrder = 0;
        int i = 0;

        while (i < entityManager.characterList.Count)
        {
            if (entityManager.characterList[i].isEntityDead() == false)
            {
                ChangeSelectedPlayerCharacter(entityManager.characterList[i]);
                
                break;
            }
            currentPlayerOrder++;
            i++;
        }

        if (i == entityManager.characterList.Count)
        {
            //PLAYERS HAVE LOST
            //activate lose screen
            Debug.Log("Lost!");
        }
    }

    public void ResetHighlightedEntity()
    {
        uiManager.highlightedTarget = entityManager.enemyList[0];
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
