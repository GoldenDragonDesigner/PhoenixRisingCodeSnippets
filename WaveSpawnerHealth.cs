using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author:  Benjamin Boese
//Date: 11-6-2019
//Purpose: For making a wave spawner have health being a stand alone wave spawner
public class WaveSpawnerHealth : MonoBehaviour
{
    [Header("The health bar slider goes here")]
    public Slider waveSpawnSlider;//the slider for the health bar goes here

    [Header("The health of the wave spawner")]
    public float waveSpawnCurrentHealth;//the current health of the wave spawner
    public float maxWaveSpanwHealth;//the max amount of the wave spawner health

    [Header("The death effects of the wave spawner")]
    public GameObject deathEffects;//the death effect game object prefab goes here
    public Transform deathLocation;//the transform of the parent game object goes here for when the spawner is destroyed

    [Header("Is the wave spawner dead?")]
    public bool waveSpawnDead;//the bool for if the wave spawner is dead or not

    [Header("Is the wave spanwer being attacked?")]
    public bool waveSpawnAttacked;//the bool for if the wave spawner is being attacked or not

    [Header("The wave spawner Game object goes here")]
    public GameObject waveSpawn;//the game object for the wave spawner

    // Start is called before the first frame update
    void Start()
    {
        waveSpawnSlider.value = CalculatingWaveSpawnHealth();//setting the wave spawner health at start
        waveSpawnDead = false;//setting if the wave spawner is dead bool to false at start
        waveSpawnAttacked = false;//setting if the wave spawner is being attacked to false at start
    }

    // Update is called once per frame
    void Update()
    {
        waveSpawnSlider.value = CalculatingWaveSpawnHealth(); //calculating the wave spawner health in update
    }

    private void OnCollisionEnter(Collision collision) //this is the function for if the player is damaging the wave spawner
    {
        if(collision.gameObject.GetComponent<PlayerDamage>() != null)
        {
            Debug.Log("wave spawn is being attacked");
            waveSpawnAttacked = true;

            if(waveSpawnAttacked == true)
            {
                waveSpawnCurrentHealth -= collision.gameObject.GetComponent<PlayerDamage>().damage;
                if(waveSpawnCurrentHealth <= 0)
                {
                    GameObject deathFx = (GameObject)Instantiate(deathEffects, deathLocation.position, deathLocation.rotation);
                    Destroy(deathFx, 2f);

                    Destroy(waveSpawn);
                    waveSpawn = null;
                }
            }
        }
    }

    float CalculatingWaveSpawnHealth()//this calculates the wave spawer health
    {
        return waveSpawnCurrentHealth / maxWaveSpanwHealth;
    }
}
