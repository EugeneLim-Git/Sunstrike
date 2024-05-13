using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Skills/Damage Skill", order = 2)]
public class DamageSkill : ScriptableObject
{
    [SerializeField] protected BaseSkill skillStats;
}
