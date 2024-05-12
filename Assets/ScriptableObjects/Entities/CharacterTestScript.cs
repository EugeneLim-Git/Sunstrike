using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTestScript : MonoBehaviour
{
    [SerializeField] BaseCharacter base_Character;

    private float character_MaxHealth;
    private float character_HealthModifier;

    // Start is called before the first frame update
    void Start()
    {
        character_HealthModifier = 10;
        character_MaxHealth = base_Character.GetBaseHealth() + character_HealthModifier;
        Debug.Log(character_MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
