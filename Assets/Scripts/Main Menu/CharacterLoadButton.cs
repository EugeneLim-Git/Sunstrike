using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLoadButton : MonoBehaviour
{
    [SerializeField] public CharacterLoaderSO characterStored;
    private CharacterManager cManager;
    public Image background;

    // Start is called before the first frame update
    void Start()
    {
        background.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        cManager = FindObjectOfType<CharacterManager>();
        if (characterStored != null)
        {
            cManager.OnCharacterSelected(characterStored);
        }
    }
}
