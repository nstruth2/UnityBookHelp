using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CH4ANDCH5UFZP : MonoBehaviour
{
bool startDeleteMessage;
    float timer;
    int score = 0;
    int nbPetrolCansCollected;
    public GameObject plane;
    GameObject item1;
    GameObject item2;
    GameObject item3;
    GameObject item4;
    public AudioClip pickupSound;

    void Start()
    {
        GameObject.Find("scoreUI").GetComponent<Text>().text = "";
        nbPetrolCansCollected = 0;
        timer = 0.0f;
        startDeleteMessage = false;
        if (GameObject.Find("plane") != null)
        {
            plane = GameObject.Find("plane");
            plane.SetActive(false);
        }
        if (SceneManager.GetActiveScene().name == "outdoor")
        {
            item1 = GameObject.Find("item1");
            item2 = GameObject.Find("item2");
            item3 = GameObject.Find("item3");
            item4 = GameObject.Find("item4");
            item1.SetActive(false);
            item2.SetActive(false);
            item3.SetActive(false);
            item4.SetActive(false);
        }
    }

    void Update()
    {
        if (startDeleteMessage == true)
        {
            timer = timer + Time.deltaTime;
            if (timer >= 2)
            {
                displayMessageToUser("");
                timer = 0.0f;
                startDeleteMessage = false;
            }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
    string collidedTag = hit.collider.gameObject.tag;

    if (collidedTag == "petrol_can" || collidedTag == "pick_me")
    {
        if (collidedTag == "petrol_can")
            {
                nbPetrolCansCollected++;
                if (nbPetrolCansCollected == 1) item1.SetActive(true);
                if (nbPetrolCansCollected == 2) item2.SetActive(true);
                if (nbPetrolCansCollected == 3) item3.SetActive(true);
                if (nbPetrolCansCollected == 4) item4.SetActive(true);

                displayMessageToUser("You collected " + nbPetrolCansCollected + " Can(s)!");
                Destroy(hit.collider.gameObject); // Collecting the can
                gameObject.GetComponent<AudioSource>().clip = pickupSound;
                gameObject.GetComponent<AudioSource>().Play();
            }
            if(collidedTag == "pick_me")
            {
            score++;
            displayMessageToUser("You collected " + score + " Boxe(s)!");
            Destroy(hit.collider.gameObject);
            if (score >= 3)
            {
            SceneManager.LoadScene("outdoor");
            }
            }
        }

        startDeleteMessage = true;
        if (collidedTag == "plane" && nbPetrolCansCollected >= 4)
        {
            displayMessageToUser("Well done. You can now fly the plane and leave the island");
            Destroy(GameObject.Find("AircraftJet"));
            plane.SetActive(true);
            gameObject.SetActive(false);
            startDeleteMessage = true;
        }
        else if (collidedTag == "plane")
        {
            displayMessageToUser("Sorry You Need 3 Cans to Fly the Plane");
            startDeleteMessage = true;
        }
    }
    

    void displayMessageToUser(string messageToDisplay)
    {
        GameObject.Find("userMessageUI").GetComponent<Text>().text = messageToDisplay;
        startDeleteMessage = true;
    }
}
