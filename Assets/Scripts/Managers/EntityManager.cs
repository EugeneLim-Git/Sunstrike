using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EntityManager : MonoBehaviour
{
    public SystemManager systemManager;

    [Header("Combat Data")]
    public List<BattleEntity> characterList;
    private List<GameObject> playerGameObjectList;
    public List<Transform> playerPositions;
    public List<BattleEntity> enemyList;
    public List<Transform> enemyPositions;
    public BattleEntity selectedCharacter;

    public List<EncounterData> commonEncounterData;
    public List<EncounterData> bossEncounterData;

    public int encounterNumber;

    public void Initialise()
    {
        enemyList = new List<BattleEntity>();
        encounterNumber = 0;
        playerGameObjectList = systemManager.characterManager.ReturnCharacterPrefabs();

        int i = 0;
        foreach (var character in playerGameObjectList)
        {
            GameObject guyToAdd = Instantiate(character);
            
            guyToAdd.GetComponent<BattleEntity>().skillList = systemManager.characterManager.selectedCharacterList[i].selectedSkillList;
            characterList.Add(guyToAdd.GetComponent<BattleEntity>());
            i++;
        }

        int iteration = 0;
        Debug.Log(characterList[0]);
        foreach (var character in characterList) 
        {
            Debug.Log(characterList[iteration]); //for debugging purposes to see who is in the list
            characterList[iteration].transform.position = playerPositions[iteration].transform.position;
            character.Initialise();
            iteration++;
        }
        iteration = 0;
        
        
        selectedCharacter = characterList[0];
        SetEnemyEncounter();
    }
    
    public List<BattleEntity> GetEntityList()
    {
        return characterList;
    }

    public void SetEnemyEncounter()
    {
        encounterNumber++;
        if (encounterNumber != 3 && encounterNumber != 6) //use common encounter Data
        {
            EncounterData newEnemyList = commonEncounterData[Random.Range(0, commonEncounterData.Count)];

            InitialiseEnemyEncounter(newEnemyList);
        }
        else if (encounterNumber == 3) //cultist boss
        {
            EncounterData newEnemyList = bossEncounterData[0];
            InitialiseEnemyEncounter(newEnemyList);
        }
        else if (encounterNumber == 6) // spider boss
        {
            EncounterData newEnemyList = bossEncounterData[1];
            InitialiseEnemyEncounter(newEnemyList);
        }
    }

    public void InitialiseEnemyEncounter(EncounterData listToLoad)
    {
        enemyList.Clear();
        int i = 0;
        foreach (var enemy in listToLoad.encounterList)
        {
            GameObject enemyToAdd = Instantiate(enemy, this.transform);
            enemyToAdd.transform.position = enemyPositions[i].transform.position;
            enemyToAdd.GetComponent<BattleEntity>().Initialise();
            enemyList.Add(enemyToAdd.GetComponent<BattleEntity>());
            i++;
        }
    }

    //public void GetNewEnemyList()
    //{
    //    foreach (var enemy in enemyList)
    //    {
    //        Destroy(enemy.gameObject);
    //    }
    //    enemyList = new();
    //    encounterNumber++;
    //    if (encounterNumber == 2)
    //    {
    //        int i = 0;
    //        foreach (var enemy in enemyList2)
    //        {
    //            Debug.Log(enemy.GetComponent<BattleEntity>());
    //            GameObject enemyToAdd = Instantiate(enemy, this.transform);
    //            enemyToAdd.transform.position = enemyPositions[i].transform.position;
    //            enemyToAdd.GetComponent<BattleEntity>().Initialise();
    //            enemyList.Add(enemyToAdd.GetComponent<BattleEntity>());
    //            i++;
    //        }
    //    }
    //    else if (encounterNumber == 3)
    //    {
    //        int i = 0;
    //        foreach (var enemy in enemyList3)
    //        {
    //            Debug.Log(enemy.GetComponent<BattleEntity>());
    //            GameObject enemyToAdd = Instantiate(enemy, this.transform);
    //            enemyToAdd.transform.position = enemyPositions[i].transform.position;
    //            enemyToAdd.GetComponent<BattleEntity>().Initialise();
    //            enemyList.Add(enemyToAdd.GetComponent<BattleEntity>());
    //            i++;
    //        }
    //    }

    //    if (encounterNumber < 4)
    //    {
    //        systemManager.SetHighlightedEnemy(enemyList[0]);
    //        Debug.Log("First character: " + enemyList[0]);
    //    }
    //    else
    //    {
    //        // let system manager know the set of encounters is over and that it needs to do the roguelike system NOW
    //    }
    //}

}
