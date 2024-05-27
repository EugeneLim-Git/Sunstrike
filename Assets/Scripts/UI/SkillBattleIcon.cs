using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBattleIcon : MonoBehaviour
{
    [Header("Data")]
    public BaseSkill currentSkill;
    private string skillName;
    public Sprite skillUISprite;
    private string skillDesc;
    public Image uiImage;

    private void Start()
    {
        uiImage= GetComponent<Image>();
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
}
