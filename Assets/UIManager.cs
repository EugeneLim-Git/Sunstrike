using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Managers")]
    public EntityManager entityManager;

    [Header("UI Control Elements")]
    public List<SkillBattleIcon> skillBattleIconList;
    public TextMeshProUGUI selectedSkillUIText;
    public TextMeshProUGUI selectedSkillDescText;

    [Header("Selected Character Data")]
    public BattleEntity selectedPlayerCharacter;
    public BattleEntity selectedEnemy;

    [Header("UI Text Elements")]
    public TextMeshProUGUI selectedPlayerCharacterName;
    public TextMeshProUGUI selectedEntityName;
    public TextMeshProUGUI selectedPlayerCharacterHealth;
    public TextMeshProUGUI selectedEntityHealth;
    public TextMeshProUGUI selectedEntityStats;

    // Start is called before the first frame update
    public void Initialise()
    {
        Debug.Log(entityManager.characterList[1].GetCurrentHealth());   
        ChangeSelectedCharacter(entityManager.characterList[0]);

        SetEntityUIText(entityManager.enemyList[0]);

        foreach (var entity in skillBattleIconList)
        {
            entity.GetComponent<SkillBattleIcon>().Initialise();
        }

        selectedSkillDescText.gameObject.SetActive(false);
    }    

    public void ChangeSelectedCharacter(BattleEntity characterToSelect)
    {
        selectedPlayerCharacter = characterToSelect;
        SetActiveSkills();
        selectedPlayerCharacterName.text = characterToSelect.GetEntityName();
        string characterHealthText = (characterToSelect.GetCurrentHealth() + "/" + characterToSelect.GetMaxHealth());
        Debug.Log(characterToSelect.GetCurrentHealth() + "/" + characterToSelect.GetMaxHealth());
        selectedPlayerCharacterHealth.text = characterHealthText;

    }

    public void SetActiveSkills()
    {
        int i = 0;
        foreach (var icon in skillBattleIconList)
        {
            skillBattleIconList[i].currentSkill = selectedPlayerCharacter.skillList[i];
            skillBattleIconList[i].SetSkillDetails();
            Debug.Log(skillBattleIconList[i].name);
            i++;
        }
        selectedSkillUIText.text = "Select a skill";
        selectedSkillDescText.gameObject.SetActive(false);
    }

    public void SetCurrentSkillText(BaseSkill currentSkill)
    {
        selectedSkillUIText.text = currentSkill.GetSkillName();
        selectedSkillDescText.gameObject.SetActive(true);
        selectedSkillDescText.text = currentSkill.GetSkillDescription();
    }

    public void SetEntityUIText(BattleEntity entityHighlighted)
    {
        //Debug.Log(enemyHighlighted.GetEntityName());
        selectedEntityName.text = entityHighlighted.GetEntityName();
        string entityHealthText = (entityHighlighted.GetCurrentHealth() + "/" + entityHighlighted.GetMaxHealth());
        selectedEntityHealth.text = entityHealthText;

        selectedEntityStats.text = (
            "Physical Strength: " + entityHighlighted.GetBasePhysicalStrength() + " + " + entityHighlighted.GetPhysicalStrengthMod() + "\n"
            + "Physical Defense: " + entityHighlighted.GetBasePhysicalDefense() + " + " + entityHighlighted.GetPhysicalDefenseMod() + "\n"
            + "Magical Strength: " + entityHighlighted.GetBaseMagicalStrength() + " + " + entityHighlighted.GetMagicalStrengthMod() + "\n"
            + "Magical Defense: " + entityHighlighted.GetBaseMagicalDefense() + " + " + entityHighlighted.GetMagicalDefenseMod() + "\n"
            + "Speed: " + entityHighlighted.GetBaseSpeed() + " + " + entityHighlighted.GetSpeedMod()
            );
    }
}
