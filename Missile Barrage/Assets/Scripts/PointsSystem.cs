using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rocket")
        {
            other.gameObject.GetComponent<ClickableRocket>().points = 50;
        }

        if (other.gameObject.tag == "Nuke")
        {
            other.gameObject.GetComponent<ClickableNuke>().points = 100;
        }
    }

    //public void OnTriggerExit(Collider other)
    //{
    //	if (other.gameObject.tag == "Rocket")
    //	{
    //		other.gameObject.GetComponent<ClickableRocket>().points = 50;
    //	}

    //	if (other.gameObject.tag == "Nuke")
    //	{
    //		other.gameObject.GetComponent<ClickableNuke>().points = 100;
    //	}
    //}
}
