using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuSceneMnager : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject LettersPanel;
    public GameObject CharachtersPanel;

    public bool isTracingGame = false;

    public bool BackToAllLetters=false;

    private void Start()
    {
        BackToAllLetters = PlayerPrefs.GetString("BackToAllLetters", "false").Equals("true");
        isTracingGame = PlayerPrefs.GetString("isTracingGame") == "true";
        if (BackToAllLetters)
        {
            LettersPage();
        }
        else
        {
            MainMenuPage();
        }
    }


    public void GoToGamePlay(int index)
    {
        if (SoundManager.inst)
        {
            SoundManager.inst.PlayAudioToLetter(index);
        }

        globalIndex = index;
 
        if (isTracingGame)
        {
 
            WritingHandler.currentLetterIndex = globalIndex;
            SceneManager.LoadScene("AlphabetWriting");
        }
        else
        {
 
            CharachtersPage();

        }
    }

    int globalIndex;
    public void GoToGamePlayScene()
    {
        if (isTracingGame)
        {
            WritingHandler.currentLetterIndex = globalIndex;
            SceneManager.LoadScene("AlphabetWriting");
        }
        else
        {

            Alphabets val = (Alphabets)globalIndex;
            SharedData.inst.selectedLetter = val;
            SceneManager.LoadScene("SimpleMovement");
    
        }
    }
    public void MainMenuPage()
    {
        PlayerPrefs.SetString("BackToAllLetters", "false");
        MainMenuPanel.SetActive(true);
        LettersPanel.SetActive(false);
        CharachtersPanel.SetActive(false);
    }

    public void LettersPage()
    {
        PlayerPrefs.SetString("BackToAllLetters", "false");
        MainMenuPanel.SetActive(false);
        LettersPanel.SetActive(true);
        CharachtersPanel.SetActive(false);
    }


    public void CharachtersPage()
    {
        PlayerPrefs.SetString("BackToAllLetters", "false");
        MainMenuPanel.SetActive(false);
        LettersPanel.SetActive(false);
        CharachtersPanel.SetActive(true);
    }

    public void SelectCar()
    {
        PlayerPrefs.SetString("SelectCharacter", "car");
        GoToGamePlayScene();
    }

    public void SelectUnicorn()
    {
        PlayerPrefs.SetString("SelectCharacter", "unicorn");
        GoToGamePlayScene();
    }


    public void SetTracingGameVal(bool state)
    {
        isTracingGame = state;
        PlayerPrefs.SetString("isTracingGame", state.ToString().ToLower());
    }
}
