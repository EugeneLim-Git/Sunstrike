using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Characters/StormDuelist", order = 3)]

public class StormDuelist : BaseCharacter
{
    [Header("Special Stats")]
    public float stormMultiplier = 1.2f;  // buffs and debuffs are stronger on this characters

      
}
