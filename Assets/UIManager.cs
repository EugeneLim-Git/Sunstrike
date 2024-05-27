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
    public TextMeshProUGUI selectedSkillName; 

    [Header("Selected Character Data")]
    public BattleEntity selectedCharacter;
    public string selectedCharacterName;
    private BaseSkill selectedSkill;

    // Start is called before the first frame update
    void Start()
    {
        selectedCharacter = entityManager.characterList[0];
        int i = 0;
        foreach (var icon in skillBattleIconList)
        {
            skillBattleIconList[i].currentSkill = selectedCharacter.skillList[i];
            skillBattleIconList[i].SetSkillDetails();
            Debug.Log(skillBattleIconList[i].name);
            i++;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
