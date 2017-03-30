using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject continueButton;

    // Use this for initialization
    void Start()
    {
        int continueBool = PlayerPrefs.GetInt("Continue?");
        if (continueBool >= 1)
        {
            continueButton.GetComponent<Button>().interactable = true;
            continueButton.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            GameObject tempText = continueButton.GetComponentInChildren<Text>().gameObject;

            tempText.GetComponent<Text>().color = new Color(0, 0, 0, 255);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("Level Number", 1);
        PlayerPrefs.SetInt("Continue?", 0);
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1.0f;
    }

    public void ContinueGame()
    {
        int levelNumber = PlayerPrefs.GetInt("Level Number");
        levelNumber += 1;
        string level = "Level " + levelNumber;
        PlayerPrefs.SetInt("Level Number", levelNumber);
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1.0f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}