using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BaseCharacter", order = 1)]
public class BaseCharacter : ScriptableObject
{
    [Header("Character Information")]
    [SerializeField] protected string character_Name;
    [SerializeField] protected string character_Desc;
    [SerializeField] protected Sprite character_BaseSprite;
    [SerializeField] protected Animator character_Animator;
    [SerializeField] AnimatorController character_AnimController;
    //born to dilly dally forced to lock in. . .

    [Header("Combat Statistics")]
    [SerializeField] protected float entity_Health;
    [SerializeField] protected float entity_PhysicalStrength;
    [SerializeField] protected float entity_PhysicalDefense;
    [SerializeField] protected float entity_MagicalStrength;
    [SerializeField] protected float entity_MagicalDefense;
    [SerializeField] protected float entity_Speed;
    //Physical and Magical stat-types dictate damage they take and receive from their respective types.
    //the intended gameplay manager/creator for entities will call upon these stats to insert them into characters/enemies
    //the statistical values will be used as part of an equation, rather than be directly modified for the sake of 

    //Balancing wise, this helps to create well-defined strengths and weaknesses as a baseline level,
    //as some characters or enemies are stronger or weaker on certain sides, and the game can be balanced to create
    //dynamic uses of these stats, such as boosting them or giving an enemy an abnormally high stat that players have to deal with

    public float GetBaseHealth()
    {
        return entity_Health;
    }
}
