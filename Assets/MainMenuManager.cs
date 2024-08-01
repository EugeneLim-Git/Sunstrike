using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager.Initialise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
