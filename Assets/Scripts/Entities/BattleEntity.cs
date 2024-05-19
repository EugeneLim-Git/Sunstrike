using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleEntity : MonoBehaviour
{
    [Header("Character Information")]
    protected BaseCharacter baseCharacterSO;
    protected List<BaseSkill> skillList; //stores all known skills in a list

    [Header("Combat Data")]
    protected float characterMaxHealth;
    protected float characterCurrentHealth;
    protected float characterPhysicalStrength;
    protected float characterPhysicalDefense;
    protected float characterMagicalStrength;
    protected float characterMagicalDefense;
    protected float characterSpeed;

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
    }

    public float GetMaxHealth()
    {
        return characterMaxHealth;
    }

    public float GetCurrentHealth()
    {
        return characterCurrentHealth;
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

}
