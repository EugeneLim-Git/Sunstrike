using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Managers")]
    public EntityManager entityManager;

    [Header("UI Elements")]
    public List<SkillBattleIcon> skillBattleIconList;
    public TextMeshProUGUI selectedSkillUIText;

    [Header("Selected Character Data")]
    public BattleEntity selectedPlayerCharacter;
    public BattleEntity selectedEnemy;

    [Header("UI Text Elements")]
    public TextMeshProUGUI selectedPlayerCharacterName;
    public TextMeshProUGUI selectedEnemyName;
    public TextMeshProUGUI selectedPlayerCharacterHealth;
    public TextMeshProUGUI selectedEnemyHealth;
    private BaseSkill selectedSkill;

    // Start is called before the first frame update
    public void Initialise()
    {
        Debug.Log(entityManager.characterList[1].GetCurrentHealth());   
        ChangeSelectedCharacter(entityManager.characterList[0]);

        SetEnemyUIText(entityManager.enemyList[0]);

        foreach (var entity in skillBattleIconList)
        {
            entity.GetComponent<SkillBattleIcon>().Initialise();
        }
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
        SetCurrentSkillText(selectedPlayerCharacter.skillList[0]);
    }

    public void SetCurrentSkillText(BaseSkill currentSkill)
    {
        selectedSkillUIText.text = currentSkill.GetSkillName();

    }

    public void SetEnemyUIText(BattleEntity enemyHighlighted)
    {
        //Debug.Log(enemyHighlighted.GetEntityName());
        selectedEnemyName.text = enemyHighlighted.GetEntityName();
        string enemyHealthText = (enemyHighlighted.GetCurrentHealth() + "/" + enemyHighlighted.GetMaxHealth());
        selectedEnemyHealth.text = enemyHealthText;
        
    }

    public BaseSkill GetCurrentSkill()
    {
        return selectedSkill;
    }
}
