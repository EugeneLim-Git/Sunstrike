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
        Buff,
        Debuff,
        Heal
    }
    public enum SkillScaler
    {
        Physical,
        Magical,
        PhysicalDefense,
        MagicalDefense,
        Speed
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
    [SerializeField] protected string skillDesc; // determines the description of the skill when selected in the UI.
    [SerializeField] protected float skillValue; // determines the numerical value of the skill. Varies between skill types.
    [SerializeField] protected SkillType typeOfSkill; //determines the type of skill.to be used by the skill manager to then feed other values into it.
    [SerializeField] protected SkillScaler skillScalerType; //determines what stat the skill scales off of. e.g. most attacks use the Physical/Magical strength stats. Buffs can use any.
    [SerializeField] protected SkillTarget skillTargets; // determines what the skill can actually target. e.g. if it only targets allies, enemies, either/any, or all entities on the field.
    [SerializeField] protected SkillTargetRange skillRange; // determines the 'range' of what it can hit. If it hits only one thing, multiple, or all entities on the field.
    [SerializeField] protected int numOfTargets; // determines how MANY targets a skill would have specifically. applies only to 'Multiple' targets.

    [Header("Skill Visuals")]
    [SerializeField] protected Animator skillAnimator; //determines animations used
    [SerializeField] protected Sprite skillSprite; // determines the sprite used in battle.
    [SerializeField] public Sprite skillUIImage; //determines the sprite used in the game UI.
    

    //the following functions can be called to retrieve protected values
    public string GetSkillName()
    {
        return skillName;
    }

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
    public SkillScaler GetSkillScalerType()
    {
        return skillScalerType;
    }
    public int GetNumOfTargets()
    {
        return numOfTargets;
    }

    public Sprite GetBattleSprite()
    {
        return skillSprite;
    }

    public Sprite GetSkillUIImage()
    {
        return skillUIImage;
    }

    public virtual float GetAttackDamage(BattleEntity attacker, BattleEntity defender, float classMultiplier, float genericMultiplier)
    {
        float skillPower = skillValue;
        float finalDamage;

        if (skillScalerType == SkillScaler.Physical)
        {
            finalDamage = skillPower * (attacker.GetPhysicalStrength() / defender.GetMagicalDefense());
            return finalDamage;
        }
        else
        {
            finalDamage = skillPower * (attacker.GetMagicalStrength() / defender.GetPhysicalDefense());
            return finalDamage;
        }
        //uses a simple system that scales skill power off of the remainder of strength divided by defense
        //usually results in very simple or slight power increases or decreases. eg. if skill power is 20, strength is 12 and defense is 10, results in a damage multiplier of 1.2
        //final damage is equal to 20 * 1.2, which is 24
        // damage should be dealt by the combat manager
    }

    public virtual float GetHealAmount(BattleEntity healer, BattleEntity healTarget, float classMultiplier, float genericMultiplier)
    {
        float baseHealAmount = skillValue;
        float finalHealAmount;

        finalHealAmount = (baseHealAmount * genericMultiplier) * classMultiplier;
        return finalHealAmount;

    }

    public virtual float GetBuffAmount(BattleEntity buffer, BattleEntity buffTarget, float classMultiplier, float genericMultiplier)
    {
        float multiplierValue = skillValue;
        float finalMultiplierValue;

        finalMultiplierValue = (skillValue * classMultiplier) * genericMultiplier;
        return finalMultiplierValue;
    }


    //born to mine forced to craft
}
