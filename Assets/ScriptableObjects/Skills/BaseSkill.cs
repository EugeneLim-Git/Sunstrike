using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BaseSkill", order = 2)]
public abstract class BaseSkill : ScriptableObject
{
    public enum SkillType{
        Damage,
        SelfBuff,
        Buff,
        Debuff,
        Heal
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
    [SerializeField] protected string skillName;
    [SerializeField] protected SkillType typeOfSkill; // determines the type of skill. to be used by the skill manager to then feed other values into it.
    [SerializeField] protected float skillValue; // NOTE: this determines the numerical value of whatever you would be doing. e.g. 20 in this value for a damage skill would deal 20 damage. Debuffs can tie this value into chance of success.
    [SerializeField] protected SkillTarget skillTargets; // determines what the skill can actually target. e.g. if it only targets allies, enemies, either/any, or all entities on the field.
    [SerializeField] protected SkillTargetRange skillRange; // determines the 'range' of what it can hit. If it hits only one thing, multiple, or all entities on the field.
    [SerializeField] protected int numOfTargets; // determines how MANY targets a skill would have specifically. applies only to 'Multiple' targets.

    [Header("Skill Visuals")]
    [SerializeField] protected Animator skillAnimator;
    [SerializeField] protected Sprite skillSprite;
    
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

}
