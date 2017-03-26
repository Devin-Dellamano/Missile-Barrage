using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopExplosionLooping : MonoBehaviour
{
    float timer = 0.0f;
    public bool startTimer = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;

            if (timer >= 0.8f)
                Destroy(this.gameObject);
        }
    }
}