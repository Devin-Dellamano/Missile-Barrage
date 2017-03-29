using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickableNuke : MonoBehaviour
{

    int lives = 3;
    //bool doOnce = false;
    public int points = 50;
    public GameObject pointPanel;
    public GameObject explosion;
    public GameObject optionsPanel;

    float timer = 0.0f;
    public float randTimer;

    // Use this for initialization
    void Start()
    {
        //randTimer = Random.Range(0.0f, 1.0f);
        pointPanel = GameObject.FindGameObjectWithTag("Points");
        explosion = GameObject.Find("Explosion");
        optionsPanel = GameObject.FindGameObjectWithTag("Level Panel");
        optionsPanel = optionsPanel.transform.Find("Options Panel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale < 1.0f || optionsPanel.activeSelf)
            return;
        timer += Time.deltaTime;
        if (timer >= randTimer)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f, transform.position.z);
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitObj = new RaycastHit();

                if (Physics.Raycast(ray, out hitObj, 10.0f))
                {
                    if ((hitObj.transform.gameObject == this.gameObject) && lives <= 0)
                    {
                        GameObject tempSound = GameObject.FindGameObjectWithTag("NExplosion").gameObject;

                        tempSound.GetComponent<AudioSource>().Play();

                        int tempPoints = int.Parse(pointPanel.GetComponent<Text>().text);
                        tempPoints += points;
                        pointPanel.GetComponent<Text>().text = tempPoints.ToString();

                        GameObject newExplosion = Instantiate(explosion);
                        newExplosion.transform.position = this.gameObject.transform.position;
                        newExplosion.GetComponent<StopExplosionLooping>().startTimer = true;
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        lives -= 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Destruction")
        {
            GameObject tempSound = GameObject.FindGameObjectWithTag("NExplosion").gameObject;

            tempSound.GetComponent<AudioSource>().Play();

            GameObject newExplosion = Instantiate(explosion);
            newExplosion.transform.position = this.gameObject.transform.position;
            newExplosion.GetComponent<StopExplosionLooping>().startTimer = true;
        }
    }
}

