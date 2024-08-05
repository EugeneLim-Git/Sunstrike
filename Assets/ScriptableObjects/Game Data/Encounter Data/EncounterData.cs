using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Encounter Data", order = 3)]
public class EncounterData : ScriptableObject
{
    public List<GameObject> encounterList;
}
