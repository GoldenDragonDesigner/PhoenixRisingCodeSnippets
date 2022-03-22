using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:  Benjamin Boese
//Date: 11-6-2019
//Purpose: For making an exploding enemy that derives from the base enemy script
public class ExplodingEnemy : BaseEnemy
{
    [Header("Exploding Enemy Info")]
    private float timeDelay;//the float that is set for when the enemy is spawned for counting down when they are to explode
    private float countDown;//the float that is counting down to the explosion
    public GameObject blastRadius;//this is the blast radius game object so when it is activated damages the player
    public GameObject explosionFX;//the particle effect of the explosion
    [Header("Min and Max Range for when the enemy is set to explode")]
    public float minRange;//the minimum number that you can set for random explosion times
    public float maxRange;//the maximum number that you can set for random explosion times
    public bool hasExploded;//the bool for if the enemy has exploded

    [Header("The starting color for Lerp")]
    public Color startingLerpedColor = Color.yellow; //the color that starts when lerp is activated
    [Header("The ending color for Lerp")]
    public Color endinglerpedColor = Color.red; //the color that ends after lerp is activated

    private float startingLerpColorTime; //the time the lerp will start out at which will be zero
    [Header("The Max Lerp time")]
    public float maxStartingColorTime; //set this to a number you want to have lerp start at
    [Header("The blast radius PFI Game Object goes here")]
    public GameObject areaPFI; //This shows the player where the blast will happen in a radius around the enemy
    [Header("The color to Lerp number its either a 1 or a 0")]
    public float colorToLerp; //this number is set to either a one or a zero for the lerping color

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        areaPFI.SetActive(false);
        countDown = timeDelay = Random.Range(minRange, maxRange);
        blastRadius.SetActive(false);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (countDown <= 0 && !hasExploded || base.curEnemyHealth <= 0)//the if statement for counting down the bomb explosion
        {
            hasExploded = true;
            EnemyDeath();
            base.Death();
        }
        else
        {
            countDown -= Time.deltaTime;
            if (countDown <= maxStartingColorTime)
            {
                startingLerpColorTime += Time.deltaTime;
                //Debug.Log("executing the Lerp function");
                LerpingColor();
            }
        }
    }

    public void EnemyDeath()//this is the function for when the enemy has exploded and activating the blast radius to cause area damage
    {
        //Debug.Log("In the Death function first activating the blast radius then going into the if statement");
        if (hasExploded == true)
        {
            //Debug.Log("Activating the blast radius");
            blastRadius.SetActive(true);
            if (blastRadius)
            {
                //Debug.Log("Instantiating the explostion");
                GameObject explodeFX = (GameObject)Instantiate(explosionFX, deathLocation.transform.position, deathLocation.transform.rotation);
                Destroy(explodeFX, 2f);
                //Debug.Log("destroying the enemy");
            }
            Destroy(gameObject, .5f);
        }
    }

    void LerpingColor()//this is the lerping function for the PFI for when the expolding enemy has exploded
    {
        //Debug.Log("setting the PFI active");
        areaPFI.SetActive(true);
        if (areaPFI)
        {
            //Debug.Log("lerping the color of the circle child game object " + colorToLerp);
            areaPFI.gameObject.GetComponent<Renderer>().material.color = Color.Lerp(startingLerpedColor, endinglerpedColor, colorToLerp);
            colorToLerp = startingLerpColorTime / maxStartingColorTime;
            //Debug.Log("changing the colorToLerp with time " + colorToLerp);
        }

    }

    public void InstaKill()//this is the function that kills the enemy from the debug menu
    {
        Destroy(this.gameObject);
        //Debug.Log("Enemy Patrolling Instakill");
    }

    protected override void OnCollisionEnter(Collision collision)//this is the function that is being called from the base enemy for when the player is attacking this enemy
    {
        base.OnCollisionEnter(collision);
    }
}
