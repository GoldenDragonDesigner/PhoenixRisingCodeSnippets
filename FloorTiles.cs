using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:  Benjamin Boese
//Date: 11-6-2019
//Purpose: For making the floor tiles change color and damage the player
public class FloorTiles : MonoBehaviour
{
    [Header("The floor Game object goes here")]
    public GameObject floor;//the floor tile prefab goes here

    [Header("The enemy tagged as slime enemy goes here")]
    public GameObject slimeEnemyPrefab; //the slime enemy prefab that is tagged as slime enemy goes here

    [Header("The player prefab goes here")]
    public GameObject playerPrefab; //the player prefab goes here in the inspector

    [Header("The floor tile collider game object goes here")]
    public GameObject floorTileDamage;//the collider for when the floor is in its damaging state

    [Header("Has the floor been highlighted?")]
    public bool NotHighLighted; //the bool for if the floor is not hightlighted

    [Header("Is the floor in the damaged color state?")]
    public bool isDamageColor; //the bool for if the damage is colored or not

    [Header("The starting Material color for lerp")]
    public Material startingLerpedColor; //The material of the starting lerped color

    [Header("The ending Material color for lerp")]
    public Material endingLerpedColor; //the material of the ending lerp color

    [Header("Just for reference only. Lerping time counting up")]
    public float startingLerpColorTime; //the time that is counting up when lerping

    [Header("Set this for the Max amount of time for Lerping")]
    public float maxStartingLerpColorTime; //the time that is set in the inspector for how long the lerp will last for

    [Header("This should be set at 1 for the material to lerp to the ending color")]
    public float colorToLerp; //the number, either 1 or 0, for setting the lerp color

    [Header("The origional material color of the floor goes here")]
    public Material bolderFloor; //the material that the floor is at before and after lerping happens

    [Header("The damaged color material of the floor goes here")]
    public Material redFloor; //the material that the floor is when it is damaging the player

    Renderer rend; //a renderer variable to be able to set it at start

    private void Start()
    {
        rend = GetComponent<Renderer>(); //setting the rend variable to the component of renderer
        startingLerpedColor = floor.GetComponent<Renderer>().material = redFloor; //setting the starting lerp color to the redfloor material at start
        endingLerpedColor = floor.GetComponent<Renderer>().material = bolderFloor; //setting the ending lerp color to the bolder floor material at start
        //Debug.Log("does the floor do damage?");
        isDamageColor = false; //setting the is damaging color bool to false at start
        //Debug.Log("Setting the floor tiles to yellow at start");
        SetHighlight(true); //setting the setHightlight bool function to true at start
        //Debug.Log("Setting the SenemyDamage script to inactive at start");
        floorTileDamage.SetActive(false); //setting the floor tile damage collider to set active false at start
    }

    private void Update()
    {
        if (isDamageColor == true)//if the damage color bool is true start the lerping color time and run the lerping color function
        {
            //Debug.Log("counting up with the lerp color time");
            startingLerpColorTime += Time.deltaTime;
            //Debug.Log("going Into the lerping function");
            LerpingColor();
        }
        else
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)//if the slime enemy is in contact with the collider it sets the highight bool function to true
    {
        //Debug.Log("In the ontriggerenter Function before the if statement");
        if(other.gameObject.tag == "SlimeEnemy" && other.gameObject.tag != "Untagged" && other.gameObject.tag != "Player" && isDamageColor == false)
        {
            //Debug.Log("changing the is damage color to true");
            isDamageColor = true;
            if (isDamageColor == true)
            {
                //Debug.Log("Enemy has changed the floor to the color Red");
                SetHighlight(false);
            }
        }
    }

    public void SetHighlight(bool highLightOff)//the sethighlight bool function for making the floor material change colors
    {
        //Debug.Log("currently highlighted set to highlighted");
        NotHighLighted = highLightOff;
        if (NotHighLighted)
        {
            //Debug.Log("currently hightlighted is yellow");
            floor.GetComponent<Renderer>().material = bolderFloor;
        }
        else
        {
            //Debug.Log("currently highlighted is red");
            floor.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void LerpingColor()//the lerping color function that lerps between the starting and ending lerp material and setting the floor tile damage to true if the lerping is running and back to false if the lerping is over
    {
        //Debug.Log("lerping the color of the tile floor from red to yellow");
        /*floor.gameObject.GetComponent<Renderer>().material = */
        rend.material.Lerp(startingLerpedColor, endingLerpedColor, colorToLerp);
        colorToLerp = startingLerpColorTime / maxStartingLerpColorTime;
        //Debug.Log("Setting the damage volume to active");
        floorTileDamage.SetActive(true);
        if(startingLerpColorTime >= maxStartingLerpColorTime)
        {
            //Debug.Log("Resetting the starting lerp color time");
            startingLerpColorTime = 0;
            //Debug.Log("setting the hightlight back to true");
            SetHighlight(true);
            //Debug.Log("Setting the is damage bool back to false");
            isDamageColor = false;
            //Debug.Log("Setting the damage volume back to inactive");
            floorTileDamage.SetActive(false);
        }

    }
}
