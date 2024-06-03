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
    public TextMeshProUGUI selectedPlayerCharacterName;
    public TextMeshProUGUI selectedEnemyName;
    public TextMeshProUGUI selectedPlayerCharacterHealth;
    public TextMeshProUGUI selectedEnemyHealth;
    private BaseSkill selectedSkill;

    // Start is called before the first frame update
    public void Start()
    {
        ChangeSelectedCharacter(entityManager.characterList[0]);
    }

    

    public void ChangeSelectedCharacter(BattleEntity characterToSelect)
    {
        selectedPlayerCharacter = characterToSelect;
        SetActiveSkills();
        selectedPlayerCharacterName.text = characterToSelect.name;


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

    public BaseSkill GetCurrentSkill()
    {
        return selectedSkill;
    }
}
