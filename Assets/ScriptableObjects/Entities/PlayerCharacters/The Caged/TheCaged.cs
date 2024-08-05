using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Characters/TheCaged", order = 3)]

public class TheCaged : BaseCharacter
{
    [Header("Special Stats")]
    public float cagedDebuffMultiplier = 1.5f;  // debuffs are stronger on this character


}