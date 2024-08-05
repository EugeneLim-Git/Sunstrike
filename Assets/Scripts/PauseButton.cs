using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private SystemManager systemManager;
    [SerializeField] private GameObject pauseMenu;
    
    public void OnClick()
    {
        systemManager.isGamePaused = !systemManager.isGamePaused;
        if (systemManager.isGamePaused == true)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }

    }
}
