using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class DestroyObject : MonoBehaviour
{
    bool doOnce = false;
    //float gravity = 9.80665f;
    public GameObject rocket;
    public GameObject nuke;
    int levelNumber;

    GameObject tempRocket;
    GameObject tempNuke;
    public GameObject buttonPanels;
    public GameObject inGameOptionsButton;
    public GameObject inGameOptionsPanel;
    public GameObject levelText;
    public GameObject city;
    Color grayCity = new Color(1, 1, 1, 0.3f);
    Color vibrantCity = new Color(1, 1, 1, 1);

    int lives = 3;

    public GameObject pointText;
    public int points;
    public GameObject losingScreen;

    // Use this for initialization
    void Start()
    {
        points = 0;
        //Time.timeScale = 0.0f;
        StartLevel();
        inGameOptionsPanel.SetActive(false);

        int continueBool = PlayerPrefs.GetInt("Continue?");
        if (continueBool >= 1)
        {
            points = PlayerPrefs.GetInt("Points");
            pointText.GetComponent<Text>().text = points.ToString();
            levelText.GetComponent<Text>().text = "Level " + levelNumber;
        }
        else
        {
            PlayerPrefs.SetInt("Continue?", 1);
            pointText.GetComponent<Text>().text = "0";

        }
        city.GetComponent<Image>().color = vibrantCity;
    }

    public void StartLevel()
    {
        city.GetComponent<Image>().color = vibrantCity;
        doOnce = false;
        points = 0;

        levelNumber = PlayerPrefs.GetInt("Level Number");
        //Debug.Log(levelNumber);

        if ((levelNumber % 5) == 0)
        {
            for (int i = 0; i < (levelNumber % 5) + 1; i++)
            {
                Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.2f, 0.5f));
                GameObject newNuke = Instantiate(nuke);
                int positionX = Random.Range(0, Screen.width);
                newNuke.transform.localPosition = v3Pos;
                if (i == 0)
                    newNuke.GetComponent<ClickableNuke>().randTimer = 5;
                else
                    newNuke.GetComponent<ClickableNuke>().randTimer = i * 5;
            }
        }

        for (int numRockets = 0; numRockets < (levelNumber + 2); numRockets++)
        {
            Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.2f, 0.5f));
            GameObject newRocket = Instantiate(rocket);
            int positionX = Random.Range(0, Screen.width);
            newRocket.transform.localPosition = v3Pos;
            newRocket.GetComponent<ClickableRocket>().randTimer = (float)(numRockets + 1);
        }
        levelText.GetComponent<Text>().text = "Level 1";
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0.0f)
        {
            GameObject[] rocketColors = GameObject.FindGameObjectsWithTag("Rocket");
            GameObject[] nukeColors = GameObject.FindGameObjectsWithTag("Nuke");
            int i = 0;
            for (; i < rocketColors.Length; i++)
            {
                rocketColors[i].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.1f);
            }
            i = 0;
            for (; i < nukeColors.Length; i++)
            {
                nukeColors[i].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.1f);
            }
            Color tempColor = new Color(1, 1, 1, 0.5f);
            city.GetComponent<Image>().color = tempColor;
        }
    }

    private void LateUpdate()
    {
        if (inGameOptionsPanel.activeSelf)
        {
            Time.timeScale = 1.0f;
            losingScreen.SetActive(false);
            buttonPanels.SetActive(false);
            return;
        }
        if (lives <= 0)
        {
            //TODO:: pull up the losing screen
            Sprite newCity = Resources.Load<Sprite>("Images/City_Rubble");
            city.GetComponent<Image>().sprite = newCity;
            losingScreen.SetActive(true);
            GameObject[] destroyRockets = GameObject.FindGameObjectsWithTag("Rocket");
            GameObject[] destroyNukes = GameObject.FindGameObjectsWithTag("Nuke");
            int i = 0;
            for (; i < destroyRockets.Length; i++)
                DestroyImmediate(destroyRockets[i]);
            i = 0;
            for (; i < destroyNukes.Length; i++)
                DestroyImmediate(destroyNukes[i]);
            Color tempColor = new Color(1, 1, 1, 0.1f);
            city.GetComponent<Image>().color = tempColor;

            Time.timeScale = 0.0f;
            return;
        }
        tempRocket = GameObject.FindGameObjectWithTag("Rocket");

        int nukes = PlayerPrefs.GetInt("Level Number");
        if (nukes % 5 == 0)
            tempNuke = GameObject.FindGameObjectWithTag("Nuke");

        if ((!tempRocket && !tempNuke) && lives > 0)
        {
            buttonPanels.SetActive(true);
            //inGameOptionsButton.SetActive(false);
            int tempPoints = int.Parse(pointText.GetComponent<Text>().text);
            PlayerPrefs.SetInt("Points", tempPoints);

            PlayerPrefs.SetInt("Level Number", levelNumber);
            PlayerPrefs.Save();
            Color tempColor = new Color(1, 1, 1, 0.5f);
            city.GetComponent<Image>().color = tempColor;
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1.0f;
        if ((levelNumber % 3) == 0)
            ShowAd();

        Sprite newCity = Resources.Load<Sprite>("Images/City_Undamaged");
        city.GetComponent<Image>().sprite = newCity;

        city.GetComponent<Image>().color = vibrantCity;
        buttonPanels.SetActive(false);
        inGameOptionsButton.SetActive(true);

        levelNumber += 1;
        for (int numRockets = 0; numRockets < levelNumber + 2; numRockets++)
        {
            Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.2f, 0.5f));
            GameObject newRocket = Instantiate(rocket);
            int positionX = Random.Range(0, Screen.width);
            newRocket.transform.localPosition = v3Pos;
            newRocket.GetComponent<ClickableRocket>().randTimer = (float)(numRockets + 1);
        }

        if ((levelNumber % 5) == 0)
        {
            for (int i = 0; i < (int)(levelNumber / 5); i++)
            {
                Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.2f, 0.5f));
                GameObject newNuke = Instantiate(nuke);
                int positionX = Random.Range(0, Screen.width);
                newNuke.transform.localPosition = v3Pos;
                newNuke.GetComponent<ClickableNuke>().randTimer = (float)(i + 1);
            }
        }

        lives = 3;

        points = int.Parse(pointText.GetComponent<Text>().text);

        PlayerPrefs.SetInt("Points", points);

        PlayerPrefs.SetInt("Level Number", levelNumber);
        PlayerPrefs.Save();

        levelText.GetComponent<Text>().text = "Level " + levelNumber;

    }

    public void RestartLevel(bool win)
    {
        Time.timeScale = 1.0f;

        Sprite newCity = Resources.Load<Sprite>("Images/City_Undamaged");
        city.GetComponent<Image>().sprite = newCity;

        city.GetComponent<Image>().color = vibrantCity;
        doOnce = false;
        buttonPanels.SetActive(false);
        inGameOptionsButton.SetActive(true);

        levelNumber = PlayerPrefs.GetInt("Level Number");

        //if (!win)
        //    levelNumber -= 1;

        buttonPanels.SetActive(false);

        if ((levelNumber % 5) == 0)
        {
            for (int i = 0; i < (levelNumber % 5) + 1; i++)
            {
                Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.2f, 0.5f));
                GameObject newNuke = Instantiate(nuke);
                int positionX = Random.Range(0, Screen.width);
                newNuke.transform.localPosition = v3Pos;
                if (i == 0)
                    newNuke.GetComponent<ClickableNuke>().randTimer = 5;
                else
                    newNuke.GetComponent<ClickableNuke>().randTimer = i * 5;
            }
        }

        for (int numRockets = 0; numRockets < (levelNumber + 2); numRockets++)
        {
            Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.2f, 0.5f));
            GameObject newRocket = Instantiate(rocket);
            int positionX = Random.Range(0, Screen.width);
            newRocket.transform.localPosition = v3Pos;
            newRocket.GetComponent<ClickableRocket>().randTimer = (float)(numRockets + 1);
        }
        
        lives = 3;

        //points = PlayerPrefs.GetInt("Points");
        pointText.GetComponent<Text>().text = points.ToString();
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main Menu");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rocket")
        {
            lives -= 1;
            //if (gravity >= 0.0f)
            if (!doOnce)
            {
                doOnce = true;
            }
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                Handheld.Vibrate();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Nuke")
        {
            lives -= 2;
            if (!doOnce)
            {
                doOnce = true;
            }
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                Handheld.Vibrate();

            Destroy(other.gameObject);
        }

        if (lives == 2)
        {
            Sprite newCity = Resources.Load<Sprite>("Images/City_Damaged");
            city.GetComponent<Image>().sprite = newCity;
            city.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
        }
        else if (lives == 1)
        {
            Sprite newCity = Resources.Load<Sprite>("Images/City_Desolate");
            city.GetComponent<Image>().sprite = newCity;
            city.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
        }
        else if (lives <= 0)
        {
            Sprite newCity = Resources.Load<Sprite>("Images/City_Rubble");
            city.GetComponent<Image>().sprite = newCity;
            city.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }
}
