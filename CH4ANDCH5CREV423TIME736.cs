using System.Collections;
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
    public GameObject plane;
    // Start is called before the first frame update
    void Start()
    {
        nbPetrolCansCollected = 0;
        timer = 0.0f;
        startDeleteMessage = false;
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
                displayMessageToUser("");
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
        if (label == "petrol_can")
        {
            nbPetrolCansCollected = nbPetrolCansCollected + 1;
            
         displayMessageToUser("You collected " + nbPetrolCansCollected +  " Can(s)!");
            Destroy(hit.collider.gameObject); //Collecting the can
        }
        if (label == "pick_me")
        score = score +1;
        GameObject.Find("userMessageUI").GetComponent<Text>().text = "You collected " + score + " Boxe(s)!";
        if (score >= 3)
        {
            SceneManager.LoadScene("outdoor");

        }
        print("collision with "+ label);
        Destroy(hit.collider.gameObject);
        startDeleteMessage = true;
    }
    if (hit.collider.gameObject.tag == "plane")
        {
            if (nbPetrolCansCollected < 3)
            {
                displayMessageToUser("Sorry You Need 3 Cans to Fly the Plane");
                startDeleteMessage = true;
            }
            else
            {
                displayMessageToUser("Well done. You can now fly the plane and leave the island");
                Destroy(GameObject.Find("AircraftJet"));
                plane.SetActive(true);
                gameObject.SetActive(false);
                startDeleteMessage = true;
            }
        }
}
void displayMessageToUser(string messageToDisplay)
{
GameObject.Find("userMessageUI").GetComponent<Text>().text = messageToDisplay;
startDeleteMessage = true;
}
}
