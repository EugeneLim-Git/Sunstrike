using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillBattleIcon : MonoBehaviour
{
    [Header("Data")]
    public BaseSkill currentSkill;
    private string skillName;
    public Sprite skillUISprite;
    private string skillDesc;
    public Image uiImage;

    private SystemManager systemManager;

    public void Initialise()
    {
        //uiImage = this.gameObject.GetComponent<Image>();

        systemManager = FindObjectOfType<SystemManager>();
    }

    public void SetSkillDetails()
    {
        skillUISprite = currentSkill.GetSkillUIImage();
        skillName = currentSkill.GetSkillName();
        uiImage.sprite = skillUISprite;
        Debug.Log(uiImage.sprite);
    }

    public Sprite GetSkillUISprite()
    { return skillUISprite; }
    public string GetSkillName()
    { return skillName; }
    public string GetSkillDescription()
    { return skillDesc; }

    public void OnBeingClicked()
    {
        Debug.Log(this.name + " is being clicked!");
        if (systemManager.currentGameState == SystemManager.GameState.ACTIONSELECTION)
        {
            systemManager.SetSkillToUse(currentSkill);
            systemManager.SetGameState(SystemManager.GameState.TARGETTING);
        }
    }
}
