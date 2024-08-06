using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BattleEntity : MonoBehaviour
{
    [Header("Character Information")]
    protected BaseCharacter baseCharacterSO;
    public List<BaseSkill> skillList; //stores all known skills in a list
    protected string characterName;

    [SerializeField] protected AIModule aiModule;

    [Header("Visual Components")]
    [SerializeField] protected TargettingReticle targetReticle;
    [SerializeField] protected HighlightedReticle highlightReticle;
    [SerializeField] protected Transform damageNumberSpawnPoint;
    [SerializeField] protected Animator entityAnimator;

    [Header("Combat Data")]
    protected float characterMaxHealth;
    protected string characterDesc;
    [SerializeField] protected float characterCurrentHealth;
    protected float characterPhysicalStrength;
    protected float characterPhysicalDefense;
    protected float characterMagicalStrength;
    protected float characterMagicalDefense;
    protected float characterSpeed;
    [SerializeField] protected bool isDead = false;
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
        characterDesc = baseCharacterSO.GetCharacterDesc();
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

    public string GetEntityDescription()
    {
        return characterDesc;
    }

    public bool isEntityDead()
    {
        return isDead;
    }

    public void setEntityDeathStatus(bool booleanToSetTo)
    {
        isDead = booleanToSetTo;
    }
    public virtual float UseSkill(BaseSkill skillToUse, BattleEntity skillUser, BattleEntity targetOfSkill, float generalMultiplier)
    {
        if (skillToUse.GetSkillType() == BaseSkill.SkillType.Damage)
        {
            return skillToUse.GetAttackDamage(skillUser, targetOfSkill, characterClassMultiplier, generalMultiplier);
        }
        else if (skillToUse.GetSkillType() == BaseSkill.SkillType.Heal)
        {
            return skillToUse.GetHealAmount(skillUser, targetOfSkill, characterClassMultiplier, generalMultiplier);
        }
        else if (skillToUse.GetSkillType() == BaseSkill.SkillType.Buff)
        {
            return skillToUse.GetBuffAmount(skillUser, targetOfSkill, characterClassMultiplier, generalMultiplier);
        }
        else if (skillToUse.GetSkillType() == BaseSkill.SkillType.Debuff)
        {
            return skillToUse.GetBuffAmount(skillUser, targetOfSkill, characterClassMultiplier, generalMultiplier);

        }
        else
        {
            return 0;
        }
    }

    public virtual void OnUseSkill(BaseSkill skillUsed)
    {
        entityAnimator.Play("Attack");
    }

    public virtual void ResetToIdleAnim()
    {
        entityAnimator.SetBool("toAttack", false);
        entityAnimator.SetBool("wasHit", false);
    }

    public float GetMaxHealth()
    {
        return characterMaxHealth;
    }

    public float GetCurrentHealth()
    {
        return characterCurrentHealth;
    }

    public void TakeDamage(float damageAmount, GameObject damageNumberPrefab)
    {
        characterCurrentHealth -= damageAmount;
        characterCurrentHealth = Mathf.Round(characterCurrentHealth * 10.0f) * 0.1f;

        DamageNumber damagePrefab = Instantiate(damageNumberPrefab, damageNumberSpawnPoint).GetComponent<DamageNumber>();
        
       if (damagePrefab.gameObject.transform.localScale.x < 1)
       {
            damagePrefab.gameObject.transform.localScale = new Vector3(damagePrefab.gameObject.transform.localScale.x,
                1f, damagePrefab.gameObject.transform.localScale.z);
       }
       damagePrefab.Initialise(damageAmount);
        
        if (characterCurrentHealth <= 0)
        {
            SpriteRenderer deadCharacter = GetComponentInChildren<SpriteRenderer>();
            //deadCharacter.enabled = false;
            isDead = true;
            entityAnimator.Play("Dead");
        }
        else
        {
            //entityAnimator.SetBool("wasHit", true);
        }
    }

    public void RestoreHealth(float healAmount, GameObject healNumberPrefab)
    {
        characterCurrentHealth += healAmount;
        if (characterCurrentHealth > characterMaxHealth)
        {
            characterCurrentHealth = characterMaxHealth;
        }


        HealNumber healPrefab = Instantiate(healNumberPrefab, damageNumberSpawnPoint).GetComponent<HealNumber>();
        if (healNumberPrefab.gameObject.transform.localScale.x < 1)
        {
            healNumberPrefab.gameObject.transform.localScale = new Vector3(healNumberPrefab.gameObject.transform.localScale.x,
                1f, healNumberPrefab.gameObject.transform.localScale.z);
        }
        healPrefab.Initialise(healAmount);
    }

    public void StartReticle()
    {
        targetReticle.SetToActive();
    }
    public void StopReticle()
    {
        targetReticle.SetToInactive();
    }
    public void StartHighlighting()
    {
        highlightReticle.OnTargetHighlighted();
    }

    public void StopHighlighting()
    {
        highlightReticle.StopHighlighting();
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
        return characterMagicalStrength;
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
        newModValue = Mathf.Round(newModValue * 10.0f) * 0.1f;
        physicalStrengthMod = newModValue;
    }

    public void AddToPhysicalDefenseMod(float valueToAdd)
    {
        float newModValue = physicalDefenseMod + valueToAdd;
        newModValue = Mathf.Round(newModValue * 10.0f) * 0.1f;

        physicalDefenseMod = newModValue;
    }

    public void AddToMagicalStrengthMod(float valueToAdd)
    {
        float newModValue = magicalStrengthMod + valueToAdd;
        newModValue = Mathf.Round(newModValue * 10.0f) * 0.1f;

        magicalStrengthMod = newModValue;
    }

    public void AddToMagicalDefenseMod(float valueToAdd)
    {
        float newModValue = magicalDefenseMod + valueToAdd;
        newModValue = Mathf.Round(newModValue * 10.0f) * 0.1f;
        magicalDefenseMod = newModValue;
    }
    public void AddToSpeedMod(float valueToAdd)
    {
        float newModValue = speedMod + valueToAdd;
        newModValue = Mathf.Round(newModValue * 10.0f) * 0.1f;
        speedMod = newModValue;
    }

}
