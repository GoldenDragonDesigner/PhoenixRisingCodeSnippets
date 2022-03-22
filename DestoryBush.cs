using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:  Benjamin Boese
//Date: 10-6-2019
//Purpose: For making a destroyable object using layers
public class DestoryBush : MonoBehaviour
{
    [Header("The death effect game object prefab goes here")]
    public GameObject bushDeathEffect;//the bush death effect game object prefab goes here

    [Header("the transform of the game object goes here")]
    public Transform bushDeathLocation;

    [Header("The game object prefab goes here")]
    public GameObject bush;

    private void OnCollisionEnter(Collision collision)//the function that if a collision happens with a layer of 31 by the player or the bullet is destoryed on impact
    {

        if (collision.gameObject.tag == "Player" && bush.layer == 31)
        {
            GameObject deathFx = (GameObject)Instantiate(bushDeathEffect, bushDeathLocation.position, bushDeathLocation.rotation);
            Destroy(deathFx, 2f);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Bullet" && bush.layer == 31)
        {
            GameObject deathFx = (GameObject)Instantiate(bushDeathEffect, bushDeathLocation.position, bushDeathLocation.rotation);
            Destroy(deathFx, 2f);
            Destroy(gameObject);
        }
    }
}
