using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTriggerScript : MonoBehaviour
{
    [Header("Add the UI text here")]
    public GameObject uIText; //this is where the UI text is added that is wanting to be displayed

    [Header("For reference only.  This is set at start")]
    public GameObject playerPrefab;//this is the player prefab that is for the game

    // Start is called before the first frame update
    void Start()
    {
        playerPrefab = GameObject.FindWithTag("Player");//this is the player prefab that is set at start
        uIText.SetActive(false);//this is setting the ui text inactive at start
    }

    private void OnTriggerEnter(Collider other)//the function for if the player is in the trigger volume
    {
        if (other.CompareTag("Player"))
        {
            uIText.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)//the function for if the player is staying in the trigger volume
    {
        if (other.CompareTag("Player"))
        {
            uIText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)//the function for if the player has exited the trigger volume
    {
        if (other.CompareTag("Player"))
        {
            uIText.SetActive(false);
        }
    }
}
