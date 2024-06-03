using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleEntity : MonoBehaviour
{
    [Header("Character Information")]
    protected BaseCharacter baseCharacterSO;
    public List<BaseSkill> skillList; //stores all known skills in a list

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

    // Start is called before the first frame update
    void Start()
    {
        Initialise();
    }

    public virtual void Initialise()
    {
        characterMaxHealth = baseCharacterSO.GetBaseHealth();
        characterCurrentHealth = characterMaxHealth;
        characterPhysicalStrength = baseCharacterSO.GetBasePhysicalStrength();
        characterPhysicalDefense = baseCharacterSO.GetBasePhysicalDefense();
        characterMagicalStrength = baseCharacterSO.GetBaseMagicalStrength();
        characterMagicalDefense = baseCharacterSO.GetBaseMagicalDefense();
        characterSpeed = baseCharacterSO.GetBaseSpeed();
        characterClassMultiplier = baseCharacterSO.GetClassMultiplier();
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
        return characterMagicalStrength;
    }

    public float GetPhysicalDefense()
    {
        return characterPhysicalDefense;
    }
    public float GetMagicalStrength()
    {
        return characterMagicalStrength;
    }
    public float GetMagicalDefense()
    {
        return characterMagicalDefense;
    }
    public float GetSpeed()
    {
        return characterSpeed;
    }

    public float GetClassMultiplier()
    {
        return characterClassMultiplier;
    }
}
