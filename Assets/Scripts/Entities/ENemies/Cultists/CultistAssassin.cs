using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistAssassin : BattleEntity
{
    [Header("Character Information")]
    [SerializeField] protected BaseCharacter battleEntitySO;

    // Start is called before the first frame update
    public override void Initialise()
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

    public override void OnUseSkill(BaseSkill skillUsed)
    {
        entityAnimator.Play("Attack");
    }
}
