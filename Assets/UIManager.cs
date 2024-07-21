using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Managers")]
    public EntityManager entityManager;

    [Header("Pause Menu")]
    public GameObject pauseMenuUI;


    [Header("UI Control Elements")]
    public List<SkillBattleIcon> skillBattleIconList;
    public TextMeshProUGUI selectedSkillUIText;
    public TextMeshProUGUI selectedSkillDescText;
    public TextMeshProUGUI selectedSkillPowerText;

    [Header("Selected Character Data")]
    public BattleEntity selectedPlayerCharacter;
    public BattleEntity highlightedTarget;

    [Header("UI Text Elements")]
    public TextMeshProUGUI selectedPlayerCharacterName;
    public TextMeshProUGUI selectedEntityName;
    public TextMeshProUGUI selectedPlayerCharacterHealth;
    public TextMeshProUGUI selectedEntityHealth;
    public TextMeshProUGUI selectedEntityStats;
    private Animator selectedEntityStatsAnimator;
    private Animator selectedEntityNameTextAnimator;
    private Animator selectedEntityHealthAnimator;

    [Header("Misc")]
    public RectTransform actionTabRectTransform;
    public GameObject actionTabPrefab;
    public Animator skillBackgroundImage;

    // Start is called before the first frame update
    public void Initialise()
    {
        Debug.Log(entityManager.characterList[1].GetCurrentHealth());   
        ChangeSelectedCharacter(entityManager.characterList[0]);
        selectedEntityNameTextAnimator = selectedEntityName.GetComponent<Animator>();
        selectedEntityStatsAnimator = selectedEntityStats.GetComponent<Animator>();
        selectedEntityHealthAnimator = selectedEntityHealth.GetComponent<Animator>();

        SetEntityUIText(entityManager.enemyList[0].GetComponent<BattleEntity>());

        foreach (var entity in skillBattleIconList)
        {
            entity.GetComponent<SkillBattleIcon>().Initialise();
        }

        selectedSkillDescText.gameObject.SetActive(false);
        pauseMenuUI.gameObject.SetActive(false);
    }    

    public void ChangeSelectedCharacter(BattleEntity characterToSelect)
    {
        if (selectedPlayerCharacter != null)
        {
            selectedPlayerCharacter.StopHighlighting();
        }
        selectedPlayerCharacter = characterToSelect;
        selectedPlayerCharacter.StartHighlighting();
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
        selectedSkillPowerText.gameObject.SetActive(false);
        skillBackgroundImage.Play("Highlight");
    }

    public void SetCurrentSkillText(BaseSkill currentSkill)
    {
        selectedSkillUIText.text = currentSkill.GetSkillName();
        selectedSkillDescText.gameObject.SetActive(true);
        selectedSkillPowerText.gameObject.SetActive(true);
        selectedSkillDescText.text = currentSkill.GetSkillDescription();
        selectedSkillPowerText.text = "Power: " + currentSkill.GetSkillValue();
    }

    public void SetEntityUIText(BattleEntity entityHighlighted)
    {
        //Debug.Log(enemyHighlighted.GetEntityName());
        selectedEntityName.text = entityHighlighted.GetEntityName();
        string entityHealthText = (entityHighlighted.GetCurrentHealth() + "/" + entityHighlighted.GetMaxHealth());
        selectedEntityHealth.text = entityHealthText;

        selectedEntityNameTextAnimator.SetBool("toLoop", false);
        selectedEntityStatsAnimator.SetBool("toLoop", false);
        selectedEntityHealthAnimator.SetBool("toLoop", false);

        highlightedTarget = entityHighlighted;
        highlightedTarget.StartHighlighting();
        selectedEntityStats.text = (
            "Physical Strength: " + entityHighlighted.GetBasePhysicalStrength() + " + (" + entityHighlighted.GetPhysicalStrengthMod() + ")\n"
            + "Physical Defense: " + entityHighlighted.GetBasePhysicalDefense() + " + (" + entityHighlighted.GetPhysicalDefenseMod() + ")\n"
            + "Magical Strength: " + entityHighlighted.GetBaseMagicalStrength() + " + (" + entityHighlighted.GetMagicalStrengthMod() + ")\n"
            + "Magical Defense: " + entityHighlighted.GetBaseMagicalDefense() + " + (" + entityHighlighted.GetMagicalDefenseMod() + ")\n"
            + "Speed: " + entityHighlighted.GetBaseSpeed() + " + (" + entityHighlighted.GetSpeedMod() + ")"
            );

        selectedEntityNameTextAnimator.Play("SelectedAnimation");
        selectedEntityStatsAnimator.Play("SelectedAnimation");
        selectedEntityHealthAnimator.Play("SelectedAnimation");
        selectedEntityNameTextAnimator.SetBool("toLoop", true);
        selectedEntityStatsAnimator.SetBool("toLoop", true);
        selectedEntityHealthAnimator.SetBool("toLoop", true);
    }

    public void CreateActionTabElement(string skillUser, string skillName)
    {
        GameObject newActionTab = Instantiate(actionTabPrefab, actionTabRectTransform);

        newActionTab.GetComponent<ActionTab>().Initialise(skillUser, skillName);
    }
}
