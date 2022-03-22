using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

//Author:  Benjamin Boese
//Date: 11-6-2019
//Purpose: For making a base enemy using inheritance and this script being the parent class
[System.Serializable]
public class BaseEnemy : MonoBehaviour
{
    [Header("Nav Info")]
    [SerializeField]
    public Transform[] navPoints;//The array for the Nav points in the scene
    [SerializeField]
    protected int navIndex = 0;//the index for incrimenting the nav points

    [Header("Has the enemy been spawned")]
    [SerializeField]
    protected bool enemySpawned = false;//The bool for if the enemy has spawned
    [Header("Is the enemy Dead")]
    protected bool enemyDead = false; //The bool for if the enemy is dead

    [Header("Enemy Health")]
    [SerializeField]
    protected float curEnemyHealth;//The current health of the enemy
    [SerializeField]
    protected float maxEnemyHealth;//the max health of the enemy should be the same as the current health of the enemy
    [SerializeField]
    protected Slider enemySlider;//the health slider goes here from the canvas

    [Header("The prefab of the enemy")]
    [SerializeField]
    protected GameObject enemyPrefab;//This is the enemy prefab

    [Header("The nav mesh agent component goes here")]
    [SerializeField]
    protected NavMeshAgent enemyNavAgent;//the enemy nav agent

    [Header("The enemy transfrom goes here")]
    [SerializeField]
    protected Transform enemyTransform;//the transform of the enemy prefab

    [Header("What state is the enemy in now")]
    [SerializeField]
    protected GlobalVariables.EnemyStates enemyStates;//the enum for the enemy states

    [Header("The player prefab goes here")]
    [SerializeField]
    protected GameObject playerPrefab; //The prefab of the player goes here

    protected Vector3 destination; //This is just showing the position of the enemy in vector 3

    [Header("Was the enemy hit?")]
    [SerializeField]
    protected bool enemyHit = false; //A bool for if the enemy has been hit

    [Header("Is the enemy alerted?")]
    [SerializeField]
    protected bool enemyAlerted = false; //a bool for if the enemy has been alerted

    [Header("Just for reference only. Nothing should be put in this box")]
    [SerializeField]
    protected float distanceFromPlayer; //This shows the distance that the player is away from the enemy

    [Header("Distance that the enemy is not engaging with the player")]
    [SerializeField]
    protected float safeDistance; //This is the float for setting the distance from the player before the enemy reacts

    [Header("The death effects for enemy go here")]
    [SerializeField]
    protected GameObject deathEffect; //the game object that will house the death effect prefab
    [SerializeField]
    protected Transform deathLocation; //the transform of the death location

    [Header("The sound info goes here")]
    [SerializeField]
    protected AudioSource dying; //The sound when the enemy dies

    [Header("Just for reference only.")]
    [SerializeField]
    protected float attackDistanceFromPlayer; //this shows the distance that the enemy is from the player for shooting
    
    [Header("The distance that the player is from the enemy for the enemy to shoot")]
    [SerializeField]
    protected float attackDistanceTooFar; //this is the float for setting the distance from the player to have the enemy shoot
    [SerializeField]
    protected float timeBetweenShots;

    [Header("Put the amount of time between shots here")]
    [SerializeField]
    protected float startTimeBetweenShots;

    [Header("The enemies bullet prefab goes here")]
    [SerializeField]
    protected GameObject enemyProjectile;

    [Header("The spawn point transform for the bullet prefab goes here")]
    [SerializeField]
    protected Transform bulletSpawnPoint;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Debug.Log("setting the enemy health at start");
        curEnemyHealth = maxEnemyHealth;
        //Debug.Log("The nav agent does not autobrake");
        enemyNavAgent.autoBraking = false;
        //Debug.Log("The starting state the enemy is in");
        enemyStates = GlobalVariables.EnemyStates.Moving;
        //Debug.Log("Enemy is not dead");
        enemyDead = false;
        //Debug.Log("Setting the enemy is alerted to false at start");
        enemyAlerted = false;
        //Debug.Log("Setting the player at start");
        playerPrefab = GameObject.FindWithTag("Player");
        //Debug.Log("Enemy hit bool is set to false at start");
        enemyHit = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Debug.Log("runing the enemy state in update");
        EnemyState();
        //Debug.Log("calculating enemy health" + curEnemyHealth);
        enemySlider.value = CalculatingEnemyHealth();
    }

    protected virtual float CalculatingEnemyHealth() //calculating the enemy health
    {
        return (curEnemyHealth / maxEnemyHealth);
    }

    //public void InstaKill() //killing the enemy instantly from the debug menu
    //{
    //    Destroy(enemyPrefab);
    //}

    protected virtual void EnemyState() //The state that the enemy is in
    {
        switch (enemyStates)
        {
            case GlobalVariables.EnemyStates.Moving:
                //Debug.Log("Enemy is Moving");
                Moving();
                break;
            case GlobalVariables.EnemyStates.Idle:
                //Debug.Log("Enemy is Idle");
                Idle();
                break;
            case GlobalVariables.EnemyStates.Chasing:
                //Debug.Log("Enemy is Chasing");
                Chase();
                break;
            case GlobalVariables.EnemyStates.RangeAttack:
                //Debug.Log("Enemy is attacking");
                RangeAttack();
                break;
        }
    }

    protected virtual void FacingTarget(Vector3 destination)//the function for the enemy to face the player when the player is within a certain distance of the enemy
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, .5f);
    }

    protected virtual void RangeAttack()//the function for when the enemy has been hit by the player to fire back at the player
    {
        attackDistanceFromPlayer = Vector3.Distance(enemyTransform.position, playerPrefab.transform.position);
        //Debug.Log("Base enemy In the ranged attack function");
        if(attackDistanceFromPlayer <= attackDistanceTooFar)
        {
            //Debug.Log("Enemy was hit bool is set to true");
            enemyHit = true;
            if(timeBetweenShots <= 0)
            {
                //Debug.Log("Base enemy Firing the enemy projectile at the player");
                Instantiate(enemyProjectile, bulletSpawnPoint.position, Quaternion.identity);
                //Debug.Log("Base enemy resetting the time between shots");
                timeBetweenShots = startTimeBetweenShots;
                FacingTarget(playerPrefab.transform.position);
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }
        }

        if(attackDistanceFromPlayer >= attackDistanceTooFar && enemyHit == true)
        {
            //Debug.Log("Base enemy setting the enemy hit to false");
            enemyHit = false;
            //Debug.Log("Base enemy going back to moving state");
            enemyStates = GlobalVariables.EnemyStates.Moving;
        }

    }

    protected virtual void Chase() //The chase state for when the player is within range of the enemy also goes back to the moving state when the player is out of range of the enemy
    {
        distanceFromPlayer = Vector3.Distance(enemyTransform.position, playerPrefab.transform.position);
        //Debug.Log("in the chasing function");
        if (enemyAlerted == true && distanceFromPlayer <= safeDistance)
        {
            enemyAlerted = true;
            destination = playerPrefab.transform.position;
            enemyNavAgent.destination = destination;
            if(timeBetweenShots <= 0)
            {
                //Debug.Log("Base enemy Firing at the player while chasing");
                Instantiate(enemyProjectile, bulletSpawnPoint.position, Quaternion.identity);
                timeBetweenShots = startTimeBetweenShots;
                FacingTarget(playerPrefab.transform.position);
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }
        }

        if(distanceFromPlayer >= safeDistance && enemyAlerted == true)
        {
            //Debug.Log("Setting the bool for enemy alterted to false");
            enemyAlerted = false;
            enemyStates = GlobalVariables.EnemyStates.Moving;
        }
        else if(enemyHit == true)
        {
            //Debug.Log("base enemy setting the enemy hit bool to true in the moving function");
            enemyHit = true;
            //Debug.Log("base enemy is going into the ranged attack");
            enemyStates = GlobalVariables.EnemyStates.RangeAttack;
        }
    }

    protected virtual void Idle() //The Idle state the enemy is in
    {
        if (enemyNavAgent != null)
        {
            float timer = 0;
            if (timer != 0)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;

                PickNextNavPoint();

                enemyStates = GlobalVariables.EnemyStates.Moving;
            }
        }
        else
        {
            return;
        }

    }

    protected virtual void Moving()//the moving state the enemy is in
    {
        enemySpawned = true;

        distanceFromPlayer = Vector3.Distance(enemyTransform.position, playerPrefab.transform.position);

        if (enemySpawned == true && !enemyNavAgent.pathPending && enemyNavAgent.remainingDistance < .5f && enemyPrefab != null)
        {
            PickNextNavPoint();
        }
        else if(distanceFromPlayer <= safeDistance)
        {
            //Debug.Log("Base Enemy is " + distanceFromPlayer + " from Player");
            //Debug.Log("setting the enemyalerted to true");
            enemyAlerted = true;
            enemyStates = GlobalVariables.EnemyStates.Chasing;
        }
    }

    protected virtual void PickNextNavPoint() //the AI picking the next nav point
    {
        if (navPoints.Length == 0)
        {
            return;
        }
        enemyNavAgent.destination = navPoints[navIndex].transform.position;
        navIndex = (navIndex + 1) % navPoints.Length;
    }

    protected virtual void Death()//the function for the death of the enemy 
    {
        if(curEnemyHealth <= 0)
        {
            enemyDead = true;
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)//the function for if the player is damaging the enemy on collision with projectiles
    {
        if(collision.gameObject.GetComponent<PlayerDamage>() != null)
        {
            curEnemyHealth -= collision.gameObject.GetComponent<PlayerDamage>().damage;
            //Debug.Log("base enemy setting the enemyHIt bool to true");
            enemyHit = true;
    
            if(curEnemyHealth <= 0)
            {
                GameObject deathFx = (GameObject)Instantiate(deathEffect, deathLocation.position, deathLocation.rotation);
                Destroy(deathFx, 2f);
    
                //Debug.Log("Playing the DeathNoise");
                DeathNoise();
                Death();
            }
            else if(enemyHit == true)
            {

                //Debug.Log("Base Enemy going into ranged attack");
                enemyStates = GlobalVariables.EnemyStates.RangeAttack;
            }
        }
    }

    protected virtual void DeathNoise()//the noise the enemy makes when they die
    {
        //Debug.Log("Playing the death noise");
        dying.Play(0);
    }
}

