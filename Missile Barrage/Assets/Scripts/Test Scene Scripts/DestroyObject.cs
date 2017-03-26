using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject levelText;

    int lives = 3;
    public GameObject buildingOne;
    public GameObject buildingTwo;
    public GameObject buildingThree;

    public GameObject pointText;
    public int points;
    public GameObject losingScreen;

    // Use this for initialization
    void Start()
    {
        points = 0;
        //Time.timeScale = 0.0f;
        StartLevel();

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
    }

    public void StartLevel()
    {
        doOnce = false;
        points = 0;

        levelNumber = PlayerPrefs.GetInt("Level Number");
        //Debug.Log(levelNumber);

        if ((levelNumber % 5) == 0)
        {
            for (int i = 0; i < (levelNumber % 5) + 1; i++)
            {
                Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.1f, 1.0f));
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
            Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.1f, 1.0f));
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

    }

    private void LateUpdate()
    {
        if (lives <= 0)
        {
            //TODO:: pull up the losing screen
            losingScreen.SetActive(true);
            GameObject[] destroyRockets = GameObject.FindGameObjectsWithTag("Rocket");
            GameObject[] destroyNukes = GameObject.FindGameObjectsWithTag("Nuke");
            int i = 0;
            for (; i < destroyRockets.Length; i++)
                DestroyImmediate(destroyRockets[i]);
            i = 0;
            for (; i < destroyNukes.Length; i++)
                DestroyImmediate(destroyNukes[i]);

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
            inGameOptionsButton.SetActive(false);
            int tempPoints = int.Parse(pointText.GetComponent<Text>().text);
            PlayerPrefs.SetInt("Points", tempPoints);

            PlayerPrefs.SetInt("Level Number", levelNumber);
            PlayerPrefs.Save();
        }
    }

    public void NextLevel()
    {
        buttonPanels.SetActive(false);
        inGameOptionsButton.SetActive(true);

        levelNumber += 1;
        for (int numRockets = 0; numRockets < levelNumber + 2; numRockets++)
        {
            Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.1f, 1.0f));
            GameObject newRocket = Instantiate(rocket);
            int positionX = Random.Range(0, Screen.width);
            newRocket.transform.localPosition = v3Pos;
            newRocket.GetComponent<ClickableRocket>().randTimer = (float)(numRockets + 1);
        }

        if ((levelNumber % 5) == 0)
        {
            for (int i = 0; i < (int)(levelNumber / 5); i++)
            {
                Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.1f, 1.0f));
                GameObject newNuke = Instantiate(nuke);
                int positionX = Random.Range(0, Screen.width);
                newNuke.transform.localPosition = v3Pos;
                newNuke.GetComponent<ClickableNuke>().randTimer = (float)(i + 1);
            }
        }

        lives = 3;
        buildingOne.SetActive(true);
        buildingTwo.SetActive(true);
        buildingThree.SetActive(true);

        points = int.Parse(pointText.GetComponent<Text>().text);

        PlayerPrefs.SetInt("Points", points);

        PlayerPrefs.SetInt("Level Number", levelNumber);
        PlayerPrefs.Save();

        levelText.GetComponent<Text>().text = "Level " + levelNumber;

    }

    public void RestartLevel(bool win)
    {
        Time.timeScale = 1.0f;

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
                Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.1f, 1.0f));
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
            Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.1f, 1.0f));
            GameObject newRocket = Instantiate(rocket);
            int positionX = Random.Range(0, Screen.width);
            newRocket.transform.localPosition = v3Pos;
            newRocket.GetComponent<ClickableRocket>().randTimer = (float)(numRockets + 1);
        }

        buildingOne.SetActive(true);
        buildingTwo.SetActive(true);
        buildingThree.SetActive(true);
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
            Handheld.Vibrate();

            Destroy(other.gameObject);
        }

        if (lives == 2)
        {
            buildingOne.SetActive(false);
            buildingTwo.SetActive(true);
            buildingThree.SetActive(true);
        }
        else if (lives == 1)
        {
            buildingOne.SetActive(false);
            buildingTwo.SetActive(false);
            buildingThree.SetActive(true);
        }
        else if (lives <= 0)
        {
            buildingOne.SetActive(false);
            buildingTwo.SetActive(false);
            buildingThree.SetActive(false);
        }
    }

    //void OnCollisionEnter(Collision col)
    //{
    //	if (col.gameObject.tag == "Rocket")
    //	{
    //		//if (gravity >= 0.0f)
    //		if (!doOnce)
    //		{
    //			doOnce = true;
    //		}

    //		Destroy(col.gameObject);
    //	}

    //	if (col.gameObject.tag == "Nuke")
    //	{
    //		if (!doOnce)
    //		{
    //			doOnce = true;
    //		}

    //		Destroy(col.gameObject);
    //	}

    //}

}
