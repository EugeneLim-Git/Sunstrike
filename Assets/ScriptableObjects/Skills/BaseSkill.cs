using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BaseSkill", order = 2)]
public class BaseSkill : ScriptableObject
{
    public enum SkillType{
        Damage,
        SelfBuff,
        Buff,
        Debuff,
        Heal
    }
    public enum SkillScaler
    {
        Physical,
        Magical
    }
    public enum SkillTargetRange
    {
        Single,
        Multiple,
        All
    }
    public enum SkillTarget
    {
        Friendly,
        Enemy,
        Any,
        All
    }

    [Header("Skill Data")]
    [SerializeField] protected string skillName; //Name of the skill
    [SerializeField] protected float skillValue; // determines the numerical value of the skill. Varies between skill types.
    [SerializeField] protected SkillType typeOfSkill; //determines the type of skill.to be used by the skill manager to then feed other values into it.
    [SerializeField] protected SkillScaler skillScalerType; //determines if the skill uses the Physical or Magical stat, and targets the respective defense stat.
    [SerializeField] protected SkillTarget skillTargets; // determines what the skill can actually target. e.g. if it only targets allies, enemies, either/any, or all entities on the field.
    [SerializeField] protected SkillTargetRange skillRange; // determines the 'range' of what it can hit. If it hits only one thing, multiple, or all entities on the field.
    [SerializeField] protected int numOfTargets; // determines how MANY targets a skill would have specifically. applies only to 'Multiple' targets.

    [Header("Skill Visuals")]
    [SerializeField] protected Animator skillAnimator; //determines animations used
    [SerializeField] protected Sprite skillSprite; // determines the sprite used in battle.
    [SerializeField] protected Sprite skillUIImage; //determines the sprite used in the game UI.
    

    //the following functions can be called to retrieve protected values
    public SkillType GetSkillType()
    {
        return typeOfSkill;
    }
    public float GetSkillValue()
    {
        return skillValue;
    }
    public SkillTargetRange ReturnTargetRange()
    {
        return skillRange;
    }
    public SkillTarget ReturnSkillTargets()
    {
        return skillTargets;
    }
    public int GetNumOfTargets()
    {
        return numOfTargets;
    }

    public Sprite GetBattleSprite()
    {
        return skillSprite;
    }

    public Sprite GetUISPrite()
    {
        return skillUIImage;
    }

    //born to mine forced to craft
}
