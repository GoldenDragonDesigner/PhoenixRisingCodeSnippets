using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

//Author:  Benjamin Boese
//Date: 11-6-2019
//Purpose: For making a boss
public class BossSphere : MonoBehaviour
{
    [Header("Health of Boss")]
    public float bossHealth; //The bosses actual health
    public float maxBossHealth; //the max amount of the bosses health
    public float halfLife; //The number that is calculated when the bosses health is at half

    [Header("Health Bar slider for boss goes here")]
    public Slider bossHealthBar; //The slider of the health bar

    [Header("Death Effects go here")]
    public GameObject deathEffect; //the death effect of the prefab is here
    public Transform deathLocation;//the death locations transform is here

    [Header("Is the boss being attacked?")]
    public bool bossBeingAttacked = false; //The bool for if the boss is being attacked

    [Header("The nav points and nav Index for Boss movment")]
    public Transform[] navPoints; //The array that holds the nav points
    public int navIndex = 0;//The index for what nav point the boss is in 

    [Header("The Parent boss prefab and transfrom goes here")]
    public GameObject bossPrefab;//the boss Parent prefab goes here it has all the functionality on it
    public Transform bossTransform; //the transform of the parent boss prefab

    [Header("The big boss prefab and transform goes here")]
    public GameObject bigBossPrefab; //The big boss prefab that appears when its life is at half
    public GameObject smallBossPrefab; //the small boss prefab for when the player is first fighting him

    [Header("The nav mesh agents goes here")]
    public NavMeshAgent bossAgent; //the nav mesh agent for this boss

    [Header("the enum for the bosses current state")]
    public GlobalVariables.BossStates bossStates; //the enum for what state the boss is in

    [Header("The player prefab and transform goes here")]
    public GameObject playerPrefab; //the player prefab is here and it set at start
    public Transform playerTransform; //the player transform and is set at start

    [Header("The bosses projectile goes here")]
    public GameObject smallBossProjectile; //the projectile that the small boss fires
    public GameObject bigBossProjectile; //The projectile that the big boss fires

    [Header("The projectile of the bosses right spawn point goes here")]
    public Transform smallBossProjectileSpawnPointOne; //the transform spawn point for the small boss projectile that is on the right side of the boss
    public Transform bigBossProjectileSpawnPointOne; //The transform spawn point for the big boss projectile that is on the right side of the boss

    [Header("The projectile of the bosses left spawn point goes here")]
    public Transform smallBossProjectileSpawnPointTwo; //the transform spawn point for the small boss projectile that is on the left side of the boss
    public Transform bigBossProjectileSpawnPointTwo; //the transform spawn point for the big boss projectile that is in the left side of the boss

    [Header("The time between shots.  Needs to be set")]
    public float startTimeBetweenShots; //This is set in the inspector for the amount of time between the shots being fired by the boss

    [Header("Just for reference only. This is the time that is counting down")]
    public float timeBetweenShots; //this is just for reference only for the time counting down between shots

    [Header("For reference only.  This is the distance the player is from the boss")]
    public float distanceFromPlayer; //This is just displaying how far the boss is from the player

    [Header("Set this distance for when the boss will be alerted within a certain range of the player")]
    public float safeDistance; //this needs to be set in the inspector for how far away the player needs to be befor the boss will chase the player

    [Header("Has the boss been alerted?")]
    public bool bossAlerted = false; //the bool for if the boss has been alerted

    [Header("For reference only.  It is the movement of the enemy towards the player")]
    public Vector3 destination; //This is the vector 3 for the position of the enemy in the scene while moving towards the player

    [Header("Is the boss dead?")]
    public bool bossDead = false; //The bool for if the boss is dead

    [Header("Just for reference only, for the time at the nav point")]
    public float timeAtNavPoint; //the float amount of time at the nav point
    
    [Header("Just for reference only")]
    public float results; //This is the calculation for the percentage of when the boss is at half or what ever percentage is needed

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("setting the stopping distance of the boss at start");
        bossAgent.stoppingDistance = 0.5f;
        //Debug.Log("setting the boss health to the max boss health at start");
        bossHealth = maxBossHealth;
        //Debug.Log("setting the boss health bar value to calculating boss health at start");
        bossHealthBar.value = CalculatingBossHealth();
        //Debug.Log("setting the boss dead bool to false at start");
        bossDead = false;
        //Debug.Log("setting the boss alerted bool to false at start");
        bossAlerted = false;
        //Debug.Log("setting the player prefab at start");
        playerPrefab = GameObject.FindWithTag("Player");
        //Debug.Log("setting the player transform at start");
        playerTransform = GameObject.FindWithTag("Player").transform;
        //Debug.Log("Setting the boss being attacked bool to false at start");
        bossBeingAttacked = false;
        //Debug.Log("setting the boss state to moving at start");
        bossStates = GlobalVariables.BossStates.Moving;
        //Debug.Log("Setting the small boss inactive at start");
        bigBossPrefab.SetActive(false);
        //Debug.Log("calculating the results of half life at start");
        results = maxBossHealth - (maxBossHealth * 50f / 100f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Calculating the small boss health in Update");
        bossHealthBar.value = CalculatingBossHealth();
        //Debug.Log("continiously checking the boss state in update");
        BossState();
        //Debug.Log("Calculating the half life");
        results = maxBossHealth - (maxBossHealth * 50f / 100f);
        //Debug.Log("Setting the halfLife to the results in Update");
        halfLife = results;
    }

    void BossState()//this is the function for what state the boss is in right now
    {
        if (!bossDead)
        {
            switch (bossStates)
            {
                case GlobalVariables.BossStates.Moving:
                    //Debug.Log("Boss Variant is in Moving State");
                    Moving();
                    break;
                case GlobalVariables.BossStates.Chase:
                    //Debug.Log("Boss Variant is in the Chase State");
                    Chase();
                    break;
                case GlobalVariables.BossStates.Attacking:
                    //Debug.Log("Boss Variant is in the Attacking State");
                    Attacking();
                    break;
                case GlobalVariables.BossStates.FirstAttack:
                    //Debug.Log("Boss is at 50% health has grown");
                    FirstAttack();
                    break;
            }
        }
    }

    void FirstAttack()//This is the function for when the big boss is attacking after its life is at half
    {
        if (bossHealth <= halfLife)
        {
            //Debug.Log("Deactivating the small boss");
            smallBossPrefab.SetActive(false);
            //Debug.Log("setactive the bigger boss sphere");
            bigBossPrefab.SetActive(true);
            //Debug.Log("Boss going back into Moving state");
            bossStates = GlobalVariables.BossStates.Moving;
        }
    }

    void Moving()//This is the function for the boss moving around from nav point to nav point
    {
        //Debug.Log("the boss is in the moving function and boss dead bool is set to false");
        bossDead = false;

        if(bossPrefab)
        {
            distanceFromPlayer = Vector3.Distance(bossTransform.position, playerTransform.position);
            //Debug.Log("The boss is moving");
            if(!bossAgent.pathPending && bossAgent.remainingDistance < .5f && !bossDead)
            {
                //Debug.Log("Boss is picking the next nav point while in the moving function");
                PickNextNavPoint();
            }
            else if(distanceFromPlayer <= safeDistance)
            {
                //Debug.Log("setting the boss alerted bool to true");
                bossAlerted = true;
                //Debug.Log("boss is switching to the chase function");
                bossStates = GlobalVariables.BossStates.Chase;
            }
        }
    }

    void Chase()//this function is for when the player is within a certain distance and the boss starts chasing the player
    {
        //Debug.Log("boss is in the chase function and has been alerted");
        if (bossPrefab)
        {
            distanceFromPlayer = Vector3.Distance(bossTransform.position, playerTransform.position);
            //Debug.Log("small boss is chasing");
            if(bossAlerted == true && distanceFromPlayer <= safeDistance)
            {
                if (smallBossPrefab)
                {
                    //Debug.Log("setting the boss alerted bool to true");
                    bossAlerted = true;
                    //Debug.Log("setting the bosses destination to where the player is");
                    destination = playerTransform.position;
                    //Debug.Log("setting the boss agents destination to the destination fo the player");
                    bossAgent.destination = destination;
                    //Debug.Log("small Boss is shooting in the chase function");
                    SmallBossShooting();
                }
                if (bigBossPrefab)
                {
                    //Debug.Log("setting the boss alerted bool to true");
                    bossAlerted = true;
                    //Debug.Log("setting the bosses destination to where the player is");
                    destination = playerTransform.position;
                    //Debug.Log("setting the boss agents destination to the destination fo the player");
                    bossAgent.destination = destination;
                    //Debug.Log("big Boss is shooting in the chase function");
                    BigBossShooting();
                }
            }
            //Debug.Log("The boss is in the chase function and has not been alerted");
            if(distanceFromPlayer >= safeDistance && bossAlerted == true)
            {
                //Debug.Log("inside the chase funtion and setting the boss alerted to false");
                bossAlerted = false;
                //Debug.Log("boss is switching to the moving function");
                bossStates = GlobalVariables.BossStates.Moving;
            }
            else if(bossBeingAttacked == true)
            {
                //Debug.Log("The boss is being attacked by player and the bool for boss being attacked is set to true");
                bossBeingAttacked = true;
                //Debug.Log("the boss is switching to the attacking function");
                bossStates = GlobalVariables.BossStates.Attacking;
            }

        }
    }

    void Attacking()//This is the state the boss is in when they are attacking the player
    {
        //Debug.Log("the boss is inside the Attacking function");

        if (bossPrefab)
        {
            distanceFromPlayer = Vector3.Distance(bossTransform.position, playerTransform.position);
            //Debug.Log("Small boss is attacking");
            if(distanceFromPlayer <= safeDistance)
            {
                //Debug.Log("The boss is being attacked in the attacking functions bool is set to true");
                bossBeingAttacked = true;
                if (smallBossPrefab)
                {
                    //Debug.Log("small Boss is shooting in the attacking function");
                    SmallBossShooting();
                }
                else if (bigBossPrefab)
                {
                    //Debug.Log("Big Boss is shooting in the attacking function");
                    BigBossShooting();
                }
            }

            if(distanceFromPlayer >= safeDistance && bossBeingAttacked == true)
            {
                //Debug.Log("setting the boss being attacked to false in the attacking function");
                bossBeingAttacked = false;
                //Debug.Log("boss is moving from the attacking function to the moving function");
                bossStates = GlobalVariables.BossStates.Moving;
            }
        }
    }

    void SmallBossShooting() //This is the function for shooting the small bosses projectiles which are smaller in size
    {
        if(timeBetweenShots <= 0)
        {
            //Debug.Log("Instantiating the small boss projectile at spawn point one");
            Instantiate(smallBossProjectile, smallBossProjectileSpawnPointOne.position, smallBossProjectileSpawnPointOne.rotation);
            //Debug.Log("Instantiating the small boss projectile at spawn point two");
            Instantiate(smallBossProjectile, smallBossProjectileSpawnPointTwo.position, smallBossProjectileSpawnPointTwo.rotation);
            //Debug.Log("resetting the time between shots");
            timeBetweenShots = startTimeBetweenShots;
            FaceTarget(playerTransform.position);
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }

    void BigBossShooting()//this is the function for shooting the big bosses projectiles which are larger in size
    {
        if(timeBetweenShots <= 0)
        {
            //Debug.Log("Instantiating the big boss projectile at spawn point one");
            Instantiate(bigBossProjectile, bigBossProjectileSpawnPointOne.position, bigBossProjectileSpawnPointOne.rotation);
            //Debug.Log("Instantiating the big boss projectile at spawn point two");
            Instantiate(bigBossProjectile, bigBossProjectileSpawnPointTwo.position, bigBossProjectileSpawnPointTwo.rotation);
            //Debug.Log("resetting the time between shots");
            timeBetweenShots = startTimeBetweenShots;
            FaceTarget(playerTransform.position);
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }

    void PickNextNavPoint()//this is the function for the boss picking the next nav point
    {
        if (bossPrefab)
        {
            //Debug.Log("boss is picking next nav point");
            //Debug.Log("Inside the PickNextNavPoint funtion");
            if (navPoints.Length == 0)
            {
                return;
            }
            //Debug.Log("setting the boss agent destination for the nav points position");
            bossAgent.destination = navPoints[navIndex].transform.position;
            //Debug.Log("setting the nav index to the for loop of the nav points lenght");
            navIndex = (navIndex + 1) % navPoints.Length;
        }
    }

    void FindDestination()//this is the function for the boss finding the next nav point in the nav point array and settin it in the nav index
    {
        if (bossPrefab)
        {
            //Debug.Log("small boss is in the find destination function");
            bossPrefab.GetComponent<NavMeshAgent>().SetDestination(navPoints[navIndex].transform.position);
        }
    }

    private void FaceTarget(Vector3 destination)//this function is so the boss faces the player when the player is within chasing or attacking distance
    {
        //Debug.Log("In the face Target function setting the vector 3 lookPos");
        Vector3 lookPos = destination - transform.position;
        //Debug.Log("setting the loosPos.y to 0");
        lookPos.y = 0;
        //Debug.Log("creating a rotation Quaternion");
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        //Debug.Log("setting the transforms rotation to quaternion.slerp");
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, .5f);
    }

    private void OnCollisionEnter(Collision collision)//this is for when the boss is being attacked or at zero health
    {
        //Debug.Log("Inside the on collision enter function");
        if(collision.gameObject.GetComponent<PlayerDamage>() != null)
        {
            //Debug.Log("if the player is attacking the boss change the bossBeingAttacked bool to true");
            bossBeingAttacked = true;
            if(bossBeingAttacked == true)
            {
                //Debug.Log("subtracting Boss Health from when the player is damaging the boss");
                bossHealth -= collision.gameObject.GetComponent<PlayerDamage>().damage;
                //Debug.Log("inside the if statement for the boss being attacked");
                if(bossHealth <= halfLife)
                {
                    //Debug.Log("Boss going into first attack");
                    bossStates = GlobalVariables.BossStates.FirstAttack;
                }
                if(bossHealth <= 0)
                {
                    //Debug.Log("Destroying the death effect if the bosses health is less than or equal to 0");
                    GameObject deathFx = (GameObject)Instantiate(deathEffect, deathLocation.position, deathLocation.rotation);
                    Destroy(deathFx, 2f);
                    //Debug.Log("Destorying the boss game object");
                    Destroy(bossPrefab);
                }
            }
        }
    }

    float CalculatingBossHealth()//This calculates the bosses health
    {
        //Debug.Log("In the calculating small boss health function");
        return bossHealth / maxBossHealth;
    }

    public void InstaKill()//This is for instantlly killing the boss from the debug menu
    {
        //Debug.Log("inside the instakill function destorying the boss game object");
        Destroy(this.gameObject);
    }
}
