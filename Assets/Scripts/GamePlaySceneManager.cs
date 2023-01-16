using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlaySceneManager : MonoBehaviour
{

    public GameObject CarObject;
    public GameObject UnicornObject;

    private void Start()
    {
        if (PlayerPrefs.GetString("SelectCharacter") == "car")
        {
            CarObject.SetActive(true);
            UnicornObject.SetActive(false);
        }
        else if (PlayerPrefs.GetString("SelectCharacter") == "unicorn")
        {
            CarObject.SetActive(false);
            UnicornObject.SetActive(true);
        }
        else
        {
            CarObject.SetActive(true);
            UnicornObject.SetActive(false);
        }


    }
    public void GoToMainMenu()
    {
        PlayerPrefs.SetString("BackToAllLetters", "false");
        SceneManager.LoadScene("Mainmenu");
    }

     

    public void LoadAllLettersPage()
    {
        PlayerPrefs.SetString("BackToAllLetters", "true");
        SceneManager.LoadScene("Mainmenu");
    }


}
