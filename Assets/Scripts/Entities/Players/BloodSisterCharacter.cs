using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSisterCharacter : BattleEntity
{
    [Header("Character Information")]
    [SerializeField] protected BloodSister bloodSisterSO;
    


    // Start is called before the first frame update
    void Start()
    {
        characterMaxHealth = bloodSisterSO.GetBaseHealth();
        characterCurrentHealth = characterMaxHealth;
        characterPhysicalStrength = bloodSisterSO.GetBasePhysicalStrength();
        characterPhysicalDefense = bloodSisterSO.GetBasePhysicalDefense();
        characterMagicalStrength = bloodSisterSO.GetBaseMagicalStrength();
        characterMagicalDefense = bloodSisterSO.GetBaseMagicalDefense();
        characterSpeed = bloodSisterSO.GetBaseSpeed();
    }

    public void ExecuteCommand(BaseSkill skillToUse, BattleEntity targetOfSkill)
    {
        bloodSisterSO.UseSkill(skillToUse, this, targetOfSkill, 1);
    }
}
