using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:  Benjamin Boese
//Date: 11-6-2019
//Purpose: For making a destroy effect
public class DestroyEffect : MonoBehaviour
{
    [Header("The death effect prefab goes here")]
    public GameObject deathEffect;//the death effect prefab goes here

    [Header("The transform of the object to destroy goes here")]
    public Transform deathLocation; //the transform location of where the death effect is going to happen

    [Header("The prefab of the object wanting to destroy goes here")]
    public GameObject waveSpawner;//the prefab of the object hat is to be destroyed

    void DeathEffect()//this function checks if the wavespawner or object wanting to destroy is null.  If it is null then instantiate the deathFX game object
                      //and then 2 seconds later destroy the deathFx and mark it as null
    {
        if(waveSpawner == null)
        {
            Debug.Log("Instaniating the death effect");
            GameObject deathFX = (GameObject)Instantiate(deathEffect, deathLocation.position, deathLocation.rotation);
            Destroy(deathFX, 2f);
            deathFX = null;
        }

    }
}
