using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoadButton : MonoBehaviour
{
    [SerializeField] private CharacterLoaderSO characterStored;
    private CharacterManager cManager;

    // Start is called before the first frame update
    void Start()
    {
        cManager = FindObjectOfType<CharacterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        cManager.OnCharacterSelected(characterStored);
    }
}
