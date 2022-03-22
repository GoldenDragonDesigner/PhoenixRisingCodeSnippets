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
    public Transform[] navPoints;
    [SerializeField]
    protected int navIndex = 0;
    [Header("Has the enemy been spawned")]
    [SerializeField]
    protected bool enemySpawned = false;
    [Header("Is the enemy Dead")]
    protected bool enemyDead = false;
    [Header("Enemy Health")]
    [SerializeField]
    protected float curEnemyHealth;
    [SerializeField]
    protected float maxEnemyHealth;
    [SerializeField]
    protected Slider enemySlider;
    [Header("The prefab of the enemy")]
    [SerializeField]
    protected GameObject enemyPrefab;
    [Header("The nav mesh agent component goes here")]
    [SerializeField]
    protected NavMeshAgent enemyNavAgent;
    [Header("The enemy transfrom goes here")]
    [SerializeField]
    protected Transform enemyTransform;
    [Header("What state is the enemy in now")]
    [SerializeField]
    protected GlobalVariables.EnemyStates enemyStates;
    [Header("The player prefab goes here")]
    [SerializeField]
    protected GameObject playerPrefab;
    protected Vector3 destination;
    [Header("Was the enemy hit?")]
    [SerializeField]
    protected bool enemyHit = false;
    [Header("Is the enemy alerted?")]
    [SerializeField]
    protected bool enemyAlerted = false;
    [Header("Just for reference only. Nothing should be put in this box")]
    [SerializeField]
    protected float distanceFromPlayer;
    [Header("Distance that the enemy is not engaging with the player")]
    [SerializeField]
    protected float safeDistance;
    [Header("The death effects for enemy go here")]
    [SerializeField]
    protected GameObject deathEffect;
    [SerializeField]
    protected Transform deathLocation;
    [Header("The sound info goes here")]
    [SerializeField]
    protected AudioSource dying;
    [Header("Just for reference only.")]
    [SerializeField]
    protected float attackDistanceFromPlayer;
    [Header("The distance that the player is from the enemy for the enemy to shoot")]
    [SerializeField]
    protected float attackDistanceTooFar;
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

    protected virtual void Start()
    {
        curEnemyHealth = maxEnemyHealth;
        enemyNavAgent.autoBraking = false;
        enemyStates = GlobalVariables.EnemyStates.Moving;
        enemyDead = false;
        enemyAlerted = false;
        playerPrefab = GameObject.FindWithTag("Player");
        enemyHit = false;
    }

    protected virtual void Update()
    {
        EnemyState();
        enemySlider.value = CalculatingEnemyHealth();
    }

    protected virtual float CalculatingEnemyHealth()
    {
        return (curEnemyHealth / maxEnemyHealth);
    }

    protected virtual void EnemyState()
    {
        switch (enemyStates)
        {
            case GlobalVariables.EnemyStates.Moving:
                Moving();
                break;
            case GlobalVariables.EnemyStates.Idle:
                Idle();
                break;
            case GlobalVariables.EnemyStates.Chasing:
                Chase();
                break;
            case GlobalVariables.EnemyStates.RangeAttack:
                RangeAttack();
                break;
        }
    }

    protected virtual void FacingTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, .5f);
    }

    protected virtual void RangeAttack()
    {
        attackDistanceFromPlayer = Vector3.Distance(enemyTransform.position, playerPrefab.transform.position);
        if(attackDistanceFromPlayer <= attackDistanceTooFar)
        {
            enemyHit = true;
            if(timeBetweenShots <= 0)
            {
                Instantiate(enemyProjectile, bulletSpawnPoint.position, Quaternion.identity);
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
            enemyHit = false;
            enemyStates = GlobalVariables.EnemyStates.Moving;
        }

    }

    protected virtual void Chase()
    {
        distanceFromPlayer = Vector3.Distance(enemyTransform.position, playerPrefab.transform.position);
        if (enemyAlerted == true && distanceFromPlayer <= safeDistance)
        {
            enemyAlerted = true;
            destination = playerPrefab.transform.position;
            enemyNavAgent.destination = destination;
            if(timeBetweenShots <= 0)
            {
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
            enemyAlerted = false;
            enemyStates = GlobalVariables.EnemyStates.Moving;
        }
        else if(enemyHit == true)
        {
            enemyHit = true;
            enemyStates = GlobalVariables.EnemyStates.RangeAttack;
        }
    }

    protected virtual void Idle()
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

    protected virtual void Moving()
    {
        enemySpawned = true;

        distanceFromPlayer = Vector3.Distance(enemyTransform.position, playerPrefab.transform.position);

        if (enemySpawned == true && !enemyNavAgent.pathPending && enemyNavAgent.remainingDistance < .5f && enemyPrefab != null)
        {
            PickNextNavPoint();
        }
        else if(distanceFromPlayer <= safeDistance)
        {
            enemyAlerted = true;
            enemyStates = GlobalVariables.EnemyStates.Chasing;
        }
    }

    protected virtual void PickNextNavPoint()
    {
        if (navPoints.Length == 0)
        {
            return;
        }
        enemyNavAgent.destination = navPoints[navIndex].transform.position;
        navIndex = (navIndex + 1) % navPoints.Length;
    }

    protected virtual void Death() 
    {
        if(curEnemyHealth <= 0)
        {
            enemyDead = true;
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerDamage>() != null)
        {
            curEnemyHealth -= collision.gameObject.GetComponent<PlayerDamage>().damage;
            enemyHit = true;
    
            if(curEnemyHealth <= 0)
            {
                GameObject deathFx = (GameObject)Instantiate(deathEffect, deathLocation.position, deathLocation.rotation);
                Destroy(deathFx, 2f);
                DeathNoise();
                Death();
            }
            else if(enemyHit == true)
            {
                enemyStates = GlobalVariables.EnemyStates.RangeAttack;
            }
        }
    }

    protected virtual void DeathNoise()
    {
        dying.Play(0);
    }
}
