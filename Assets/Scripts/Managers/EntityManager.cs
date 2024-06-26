using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EntityManager : MonoBehaviour
{
    [Header("Combat Data")]
    public List<BattleEntity> characterList;
    public List<Transform> playerPositions;
    public List<BattleEntity> enemyList;
    public List<Transform> enemyPositions;
    public BattleEntity selectedCharacter;

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
        foreach (var enemy in enemyList) 
        {
            Debug.Log(enemyList[iteration]); //for debugging purposes to see who is in the list
            enemyList[iteration].transform.position = enemyPositions[iteration].transform.position;
            enemyList[iteration].transform.localScale = enemyPositions[iteration].transform.localScale;
            enemy.Initialise();
            iteration++;
        }
        selectedCharacter = characterList[0];
    }

    public void GetEnemyList(List<BattleEntity> enemyListToSet)
    {
        int i = 0;   
        foreach(var enemy in enemyList)
        {
            enemyListToSet[i] = enemy;
            i++;
        }
    }

}
