using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    CharacterManager charManager;
    [SerializeField] TextMeshProUGUI displayText;

    public void GoToMainMenu()
    {
        if (charManager != null)
        {
            charManager = FindObjectOfType<CharacterManager>();
            Destroy(charManager.gameObject);
        }

        SceneManager.LoadScene("MainMenuScene");
    }

    public void StartGame()
    {
        charManager = FindObjectOfType<CharacterManager>();

        int readyCharacters = 0;

        foreach (CharacterLoaderSO character in charManager.selectedCharacterList)
        {
            int i = 0;
            foreach (var selectedSkill in character.selectedSkillList)
            {
                i++;
            }
            if (i == 4)
            {
                readyCharacters++;
            }
        }

        if (readyCharacters == charManager.selectedCharacterList.Count && readyCharacters != 0)
        {
            DontDestroyOnLoad(charManager);
            SceneManager.LoadScene("BattleScene");
        }
        else
        {
            FindObjectOfType<DescriptionText>().ChangeText("One of your party members does not have four skills equipped, or you have no party members.");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
