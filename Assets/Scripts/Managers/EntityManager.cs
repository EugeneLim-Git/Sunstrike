using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            iteration++;
        }
        iteration = 0;
        foreach (var enemy in enemyList) 
        {
            Debug.Log(enemyList[iteration]); //for debugging purposes to see who is in the list
            enemyList[iteration].transform.position = enemyPositions[iteration].transform.position;
            enemyList[iteration].transform.localScale = enemyPositions[iteration].transform.localScale;
            iteration++;
        }
        selectedCharacter = characterList[0];
    }

}
