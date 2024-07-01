using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleEntity : MonoBehaviour
{
    [Header("Character Information")]
    protected BaseCharacter baseCharacterSO;
    public List<BaseSkill> skillList; //stores all known skills in a list
    protected string characterName;
    [SerializeField] protected AIModule aiModule;

    [Header("Combat Data")]
    protected float characterMaxHealth;
    protected float characterCurrentHealth;
    protected float characterPhysicalStrength;
    protected float characterPhysicalDefense;
    protected float characterMagicalStrength;
    protected float characterMagicalDefense;
    protected float characterSpeed;
    protected bool isDead = false;
    protected float characterClassMultiplier;

    [Header("Stat Modifiers")]
    [SerializeField] protected float physicalStrengthMod;
    [SerializeField] protected float physicalDefenseMod;
    [SerializeField] protected float magicalStrengthMod;
    [SerializeField] protected float magicalDefenseMod;
    [SerializeField] protected float speedMod;

    // Start is called before the first frame update
    void Start()
    {
        Initialise();
    }

    public virtual void Initialise()
    {
        characterName = baseCharacterSO.GetCharacterName();
        characterMaxHealth = baseCharacterSO.GetBaseHealth();
        characterCurrentHealth = characterMaxHealth;
        characterPhysicalStrength = baseCharacterSO.GetBasePhysicalStrength();
        characterPhysicalDefense = baseCharacterSO.GetBasePhysicalDefense();
        characterMagicalStrength = baseCharacterSO.GetBaseMagicalStrength();
        characterMagicalDefense = baseCharacterSO.GetBaseMagicalDefense();
        characterSpeed = baseCharacterSO.GetBaseSpeed();
        characterClassMultiplier = baseCharacterSO.GetClassMultiplier();

        physicalStrengthMod = 0;
        physicalDefenseMod = 0;
        magicalStrengthMod = 0;
        magicalDefenseMod = 0;
        speedMod = 0;
    }

    public AIModule GetAIModule()
    {
        return aiModule;
    }


    public string GetEntityName()
    {
        return characterName;
    }

    public bool isEntityDead()
    {
        return isDead;
    }

    public void setEntityDeathStatus(bool booleanToSetTo)
    {
        isDead = booleanToSetTo;
    }

    public void ExecuteCommand(BaseSkill skillToUse, BattleEntity targetOfSkill)
    {
        baseCharacterSO.UseSkill(skillToUse, this, targetOfSkill, 1);
    }

    public virtual void OnUseSkill()
    {
        return;
    }

    public float GetMaxHealth()
    {
        return characterMaxHealth;
    }

    public float GetCurrentHealth()
    {
        return characterCurrentHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        characterCurrentHealth -= damageAmount;
    }

    public void RestoreHealth(float healAmount)
    {
        characterCurrentHealth += healAmount;
        if (characterCurrentHealth > characterMaxHealth)
        {
            characterCurrentHealth = characterMaxHealth;
        }
    }
    public float GetPhysicalStrength()
    {
        return characterPhysicalStrength + physicalStrengthMod;
    }

    public float GetPhysicalDefense()
    {
        float valueToReturn = characterPhysicalDefense + physicalDefenseMod;

        if (valueToReturn <= 0)
        {
            valueToReturn = 0.1f;
        }
        return valueToReturn;
    }
    public float GetMagicalStrength()
    {
        return characterMagicalStrength + magicalStrengthMod;
    }
    public float GetMagicalDefense()
    {
        float valueToReturn = characterMagicalDefense + magicalDefenseMod;

        if (valueToReturn <= 0)
        {
            valueToReturn = 0.1f;
        }
        return valueToReturn;
    }
    public float GetSpeed()
    {
        return characterSpeed + speedMod;
    }
    public float GetBasePhysicalStrength()
    {
        return characterPhysicalStrength;
    }

    public float GetBasePhysicalDefense()
    {
        float valueToReturn = characterPhysicalDefense;
        return valueToReturn;
    }
    public float GetBaseMagicalStrength()
    {
        return characterMagicalStrength + magicalStrengthMod;
    }
    public float GetBaseMagicalDefense()
    {
        float valueToReturn = characterMagicalDefense;
        return valueToReturn;
    }
    public float GetBaseSpeed()
    {
        return characterSpeed;
    }
    public float GetClassMultiplier()
    {
        return characterClassMultiplier;
    }
    public float GetPhysicalStrengthMod()
    {
        return physicalStrengthMod;
    }
    public float GetPhysicalDefenseMod()
    {
        return physicalDefenseMod;
    }
    public float GetMagicalStrengthMod()
    {
        return magicalStrengthMod;
    }

    public float GetMagicalDefenseMod()
    {
        return magicalDefenseMod;
    }
    public float GetSpeedMod()
    {
        return speedMod;
    }

    public void AddToPhysicalStrengthMod(float valueToAdd)
    {
        float newModValue = physicalStrengthMod + valueToAdd;

        physicalStrengthMod = newModValue;
    }

    public void AddToPhysicalDefenseMod(float valueToAdd)
    {
        float newModValue = physicalDefenseMod + valueToAdd;

        physicalDefenseMod = newModValue;
    }

    public void AddToMagicalStrengthMod(float valueToAdd)
    {
        float newModValue = physicalStrengthMod + valueToAdd;

        physicalStrengthMod = newModValue; ;
    }

    public void AddToMagicalDefenseMod(float valueToAdd)
    {
        float newModValue = magicalDefenseMod + valueToAdd;

        magicalDefenseMod = newModValue;
    }
    public void AddToSpeedMod(float valueToAdd)
    {
        float newModValue = magicalDefenseMod + valueToAdd;

        magicalDefenseMod = newModValue;
    }

}
