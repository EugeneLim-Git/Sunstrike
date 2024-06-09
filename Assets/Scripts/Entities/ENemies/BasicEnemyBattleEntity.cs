using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBattleEntity : BattleEntity
{
    [Header("Character Information")]
    [SerializeField] protected BaseCharacter battleEntitySO;



    // Start is called before the first frame update
    void Start()
    {
        characterName = battleEntitySO.GetCharacterName();
        characterMaxHealth = battleEntitySO.GetBaseHealth();
        characterCurrentHealth = characterMaxHealth;
        characterPhysicalStrength = battleEntitySO.GetBasePhysicalStrength();
        characterPhysicalDefense = battleEntitySO.GetBasePhysicalDefense();
        characterMagicalStrength = battleEntitySO.GetBaseMagicalStrength();
        characterMagicalDefense = battleEntitySO.GetBaseMagicalDefense();
        characterSpeed = battleEntitySO.GetBaseSpeed();
        characterClassMultiplier = battleEntitySO.GetClassMultiplier();
    }

    //public void ExecuteCommand(BaseSkill skillToUse, BattleEntity targetOfSkill)
    //{
    //    bloodSisterSO.UseSkill(skillToUse, this, targetOfSkill, 1);
    //}

    
}
