﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CH4AND5COLLECTOBJECTS : MonoBehaviour
{
    bool startDeleteMessage;
    float timer;
    int score;
    int nbPetrolCansCollected;
    GameObject plane;
    // Start is called before the first frame update
    void Start()
    {
        nbPetrolCansCollected = 0;
        score = 0;
        timer = 0.0f;
        startDeleteMessage = false;
        GameObject.Find("userMessageUI").GetComponent<Text>().text = "";
        if (GameObject.Find("plane") != null)
        {
            plane = GameObject.Find("plane");
            plane.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startDeleteMessage == true)
        {
            timer = timer+Time.deltaTime;
            if(timer >= 2)
            {
                GameObject.Find("userMessageUI").GetComponent<Text>().text = "";
                timer = 0.0f;
                startDeleteMessage = false;
            }
        }
    }

void OnControllerColliderHit(ControllerColliderHit hit)
{
    if (hit.collider.gameObject.tag == "pick_me" || hit.collider.gameObject.tag == "petrol_can")
    {
        string label = hit.collider.gameObject.tag;
        if (label == "petrol-can")
        {
            nbPetrolCansCollected = nbPetrolCansCollected + 1;
                GameObject.Find("userMessageUI").GetComponent<Text>().text = "Collected " + nbPetrolCansCollected + " can(s)";
            Destroy(hit.collider.gameObject); //Collecting the can
        }
        print("collision with "+ label);
        Destroy(hit.collider.gameObject);
        score = score + 1;
        GameObject.Find("userMessageUI").GetComponent<Text>().text = "You collected " + score +  " Boxe(s)!";;
        startDeleteMessage = true;
        print ("Score: " + score);
        if (label == "plane")
        {
            if (nbPetrolCansCollected < 3)
            {
                GameObject.Find("userMessageUI").GetComponent<Text>().text = "Sorry You Need 3 Cans to Fly the Plane";
                startDeleteMessage = true;
            }
            else
            {
                GameObject.Find("userMessageUI").GetComponent<Text>().text = "Well done. You can now fly the plane and leave the island";
                Destroy(GameObject.Find("AircraftJet"));
                plane.SetActive(true);
                gameObject.SetActive(false);
                startDeleteMessage = true;
            }
        }
    }
}

}
