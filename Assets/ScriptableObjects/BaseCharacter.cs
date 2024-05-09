using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BaseCharacter", order = 1)]
public class BaseCharacter : ScriptableObject
{
    [Header("Character Information")]
    public string character_Name;
    public string character_Desc;
    public Sprite character_Sprite;

    [Header("Combat Statistics")]
    [SerializeField] public float entity_Health;
    [SerializeField] public float entity_PhysicalStrength;
    [SerializeField] public float entity_PhysicalDefense;
    [SerializeField] public float entity_MagicalStrength;
    [SerializeField] public float entity_MagicalDefense;
    [SerializeField] public float entity_Speed;
    
}
