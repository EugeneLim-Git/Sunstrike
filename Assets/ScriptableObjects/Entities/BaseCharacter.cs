using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BaseCharacter", order = 1)]
public class BaseCharacter : ScriptableObject
{
    [Header("Character Information")]
    [SerializeField] protected string characterName;
    [SerializeField] protected string characterDesc;
    [SerializeField] protected Sprite characterBaseSprite;
    [SerializeField] protected Animator characterAnimator;
    [SerializeField] AnimatorController characterAnimController;
    //born to dilly dally forced to lock in. . .

    [Header("Combat Data")]
    [SerializeField] protected float entityHealth;
    [SerializeField] protected float entityPhysicalStrength;
    [SerializeField] protected float entityPhysicalDefense;
    [SerializeField] protected float entityMagicalStrength;
    [SerializeField] protected float entityMagicalDefense;
    [SerializeField] protected float entitySpeed;
    [SerializeField] protected List<BaseSkill> skillList; //stores all known skills in a list
    //Physical and Magical stat-types dictate damage they take and receive from their respective types.
    //the intended gameplay manager/creator for entities will call upon these stats to insert them into characters/enemies
    //the statistical values will be used as part of an equation, rather than be directly modified for the sake of 

    //Balancing wise, this helps to create well-defined strengths and weaknesses as a baseline level,
    //as some characters or enemies are stronger or weaker on certain sides, and the game can be balanced to create
    //dynamic uses of these stats, such as boosting them or giving an enemy an abnormally high stat that players have to deal with
    
    public float GetBaseHealth()
    {
        return entityHealth;
    }

    public float GetBasePhysicalStrength()
    {
        return entityPhysicalStrength;
    }
    public float GetBasePhysicalDefense()
    {
        return entityPhysicalDefense;
    }
    public float GetBaseMagicalStrength()
    {
        return entityMagicalStrength;
    }
    public float GetBaseMagicalDefense()
    {
        return entityMagicalDefense;
    }
    public float GetBaseSpeed()
    {
        return entitySpeed;
    }
}
