using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author:  Benjamin Boese
//Date: 11-6-2019
//Purpose: For making a wave Spanwer
public class WaveSpawner : MonoBehaviour
{
    [Header("Enemy prefab goes here")]
    public GameObject enemyPrefab; //the enemy prefab
    [Header("The Time Between Waves")]
    public float timeBetweenWaves; //the float for the time between spawning the waves of enemies
    [Header("Time Counting down for the Wave")]
    public float timeBetweenWavesLeft; //the float for how much time is left
    [Header("The Spawn Point for where the enemies will come out")]
    public Transform spawnPoint; //the transform of the spawn point
    [Header("The Enum for the different states the wave spawner is in")]
    public GlobalVariables.WaveStates waveState; //the global enum variable for the wave state
    [Header("Is the spawner waiting?")]
    public bool waiting = true;
    [Header("The array of nav points to be set up in the scene")]
    public Transform[] navPoints; //the navpoints that are to be set in the enemy prefab
    [Header("The minimum number for the time between Spawns")]
    public float minRange;
    [Header("The maximum number for the time between spawns")]
    public float maxRange;
    [Header("Start time of the time between the waves")]
    public float startTimeBetweenSpawns; //the start time between spawns
    [Header("Time counting down for between the spawns")]
    public float timeBetweenSpawns; //the time between spawns

    void Start()
    {
        timeBetweenWavesLeft = timeBetweenWaves; //setting the time left between the waves to the time between waves
        timeBetweenSpawns = startTimeBetweenSpawns = Random.Range(minRange, maxRange); //setting the time between spawns to the start time between spawns
        waveState = GlobalVariables.WaveStates.CountingDown; //setting the switch statement to counting down at start  
        
    }

    void Update()
    {
        SpawningWaves();//running the spawning waves switch statement        
    }

    void SpawningWaves()//the switch statement that switches between waiting, counting down, and spawning
    {
            switch (waveState)
            {
                case GlobalVariables.WaveStates.Waiting:
                    //Debug.Log("This is from the switch statement in the waiting state");
                    Waiting();
                    break;
                case GlobalVariables.WaveStates.CountingDown:
                    //Debug.Log("This is from the switch statement in the Counting Down state");
                    CountingDown();
                    break;
                case GlobalVariables.WaveStates.Spawning:
                    //Debug.Log("This is from the switch statement in the Spawning State");
                    Spawning();
                    break;
                case GlobalVariables.WaveStates.Death:
                    //Death();
                    break;
            }
    }

    void Spawning()//the function that spawns one enemy into the scene at a time then switches to waiting
    {
        //Debug.Log("adding one enemy to spawn: " + numOfEnemies);
        //numOfEnemies = 1;
        //Debug.Log("Adding 1 to the number of Enemies");
        if(waiting == false)
        {            
            //Debug.Log("Spawning an enemy");
            GameObject go = (GameObject) Instantiate(enemyPrefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z), Quaternion.identity);
            BaseEnemy patrol = go.GetComponent<BaseEnemy>();
            patrol.navPoints = navPoints;
            //Debug.Log("changing the bool back to true");
            waiting = true;
            //Debug.Log("incrimenting the number of enemies");
            //waveManager.GetComponent<WaveManager>().numOfEnemies++;
            //numOfEnemies++;
            //Debug.Log("Changing the state back to waiting");
            waveState = GlobalVariables.WaveStates.Waiting;
        }
    }

    void Waiting()//the waiting function that counts down between the time the last enemy was spawned this is a random 
                    //number between the minRange and maxRange numbers set in the inspector
    {
        if(spawnPoint != null)
        {
            if(timeBetweenSpawns <= 0)
            {
                //Debug.Log("inside the if statement of waiting setting the bool  to false");
                waiting = false;
                //Debug.Log("changin the state to spawning");
                waveState = GlobalVariables.WaveStates.Spawning;
                //Debug.Log("Setting the starting time between spawns right after spawing");
                timeBetweenSpawns = startTimeBetweenSpawns = Random.Range(minRange, maxRange);
            }
            else
            {
                //Debug.Log("Inside the else statement of the waiting function and setting the bool to true");
                waiting = true;
                //Debug.Log("Counting down between spawns");
                timeBetweenSpawns -= Time.deltaTime;
            }
        }
    }

    void CountingDown()//This is only called once at the very start of the scene and it counts down before starting to spawn enemies
    {
        //Debug.Log("In the count down function");
        if(timeBetweenWaves <= 0) 
        {
            //Debug.Log("Reseting the time between waves");
            timeBetweenWavesLeft = timeBetweenWaves;
            //Debug.Log("Inside the If statement in the countdown Function that switches to spawning");
            waveState = GlobalVariables.WaveStates.Spawning;
        }
        else if(timeBetweenWaves >= 0)
        {
            //Debug.Log("In the else if statement counting down function");
            timeBetweenWaves -= Time.deltaTime;
        }
    }
}
