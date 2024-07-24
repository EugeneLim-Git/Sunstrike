using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EntityManager : MonoBehaviour
{
    public SystemManager systemManager;

    [Header("Combat Data")]
    public List<BattleEntity> characterList;
    public List<Transform> playerPositions;
    public List<BattleEntity> enemyList;
    public List<Transform> enemyPositions;
    public BattleEntity selectedCharacter;

    public List<GameObject> enemyList1, enemyList2, enemyList3;
    public int encounterNumber;

    public void Initialise()
    {
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
        
        foreach (var enemy in enemyList1) 
        {
            GameObject enemyToAdd = Instantiate(enemy);
            enemyToAdd.GetComponent<BattleEntity>().Initialise();
            enemyList.Add(enemyToAdd.GetComponent<BattleEntity>());
            Debug.Log(enemyList[iteration]); //for debugging purposes to see who is in the list
            enemyList[iteration].transform.position = enemyPositions[iteration].transform.position;
            enemyList[iteration].transform.localScale = enemyPositions[iteration].transform.localScale;
            enemy.GetComponent<BattleEntity>().Initialise();
            iteration++;
        }
        selectedCharacter = characterList[0];
        encounterNumber = 1;
    }

    public void GetNewEnemyList()
    {
        foreach (var enemy in enemyList)
        {
            Destroy(enemy.gameObject);
        }
        enemyList = new();
        encounterNumber++;
        if (encounterNumber == 2)
        {
            int i = 0;
            foreach (var enemy in enemyList2)
            {
                Debug.Log(enemy.GetComponent<BattleEntity>());
                GameObject enemyToAdd = Instantiate(enemy, this.transform);
                enemyToAdd.transform.position = enemyPositions[i].transform.position;
                enemyToAdd.GetComponent<BattleEntity>().Initialise();
                enemyList.Add(enemyToAdd.GetComponent<BattleEntity>());
                i++;
            }
            
        }
        else if (encounterNumber == 3)
        {
            int i = 0;
            foreach (var enemy in enemyList3)
            {
                Debug.Log(enemy.GetComponent<BattleEntity>());
                GameObject enemyToAdd = Instantiate(enemy, this.transform);
                enemyToAdd.transform.position = enemyPositions[i].transform.position;
                enemyToAdd.GetComponent<BattleEntity>().Initialise();
                enemyList.Add(enemyToAdd.GetComponent<BattleEntity>());
                i++;
            }
        }

        if (encounterNumber < 4)
        {
            systemManager.SetHighlightedEnemy(enemyList[0]);
            Debug.Log("First character: " + enemyList[0]);
        }
        else
        {
            // let system manager know the set of encounters is over and that it needs to do the roguelike system NOW
        }
    }

}
