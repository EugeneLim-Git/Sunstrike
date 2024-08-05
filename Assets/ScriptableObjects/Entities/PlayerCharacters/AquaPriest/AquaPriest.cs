using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Characters/Aqua Priest", order = 4)]
public class AquaPriest : BaseCharacter
{
    [Header("Special Stats")]
    public float healMultiplier = 1.2f;  // heals are stronger on this character


}