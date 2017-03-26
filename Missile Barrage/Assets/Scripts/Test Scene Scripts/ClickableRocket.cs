﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickableRocket : MonoBehaviour
{
    int lives = 1;
    public int points = 25;
    public GameObject pointPanel;
    public GameObject explosion;

    float timer = 0.0f;
    public float randTimer;
    //bool doOnce = false;

    // Use this for initialization
    void Start()
    {
        //randTimer = Random.Range(0.0f, 1.0f);
        //Debug.Log(randTimer);
        pointPanel = GameObject.FindGameObjectWithTag("Points");
        explosion = GameObject.Find("Explosion");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale < 1.0f)
            return;
        timer += Time.deltaTime;

        if (timer >= randTimer)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitObj = new RaycastHit();

                if (Physics.Raycast(ray, out hitObj, 10.0f))
                {
                    if ((hitObj.transform.gameObject == this.gameObject) && lives <= 0)
                    {
                        GameObject tempSound = GameObject.FindGameObjectWithTag("RExplosion").gameObject;

                        tempSound.GetComponent<AudioSource>().Play();

                        int tempPoints = int.Parse(pointPanel.GetComponent<Text>().text);
                        tempPoints += points;
                        pointPanel.GetComponent<Text>().text = tempPoints.ToString();

                        GameObject newExplosion = Instantiate(explosion);
                        newExplosion.transform.position = this.gameObject.transform.position;
                        newExplosion.GetComponent<StopExplosionLooping>().startTimer = true;

                        //Animator newAnimation = newExplosion.transform.FindChild("ExpAnimator").GetComponent<Animator>();
                        //newAnimation.Play();

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
}
