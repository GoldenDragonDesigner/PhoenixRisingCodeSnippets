using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

//Author:  Benjamin Boese
//Date: 11-9-2019
//Purpose: For making a boss that does not grow
public class Boss_Variant : MonoBehaviour
{
    [Header("Health of Boss")]
    public float bossHealth;//The bosses actual health
    public float maxBossHealth; //the max amount of the bosses health

    [Header("Health Bar Slider for boss goes here")]
    public Slider bossHealthBar;//The slider of the health bar

    [Header("Death effects go here")]
    public GameObject deathEffect;//the death effect of the prefab is here
    public Transform deathLocation;//the death locations transform is here

    [Header("Is the boss being attacked?")]
    public bool bossBeingAttacked = false; //the bool for if the boss is being attacked

    [Header("The nav points and nav Index for Boss movement")]
    public Transform[] navPoints; //the array that holds the nav poins
    public int navIndex = 0; //the index for what nav point the boss is in

    [Header("the boss Prefab goes here")]
    public GameObject bossPrefab; //the boss prefab goes here

    [Header("The boss transform goes here")]
    public Transform bossTransform;//this is the bosses transform

    [Header("The nav Mesh Agent goes here")]
    public NavMeshAgent bossAgent;//this is the bosses nav mesh agent slot

    [Header("The enum for the bosses current state")]
    public GlobalVariables.BossStates bossStates;//this is the enum for what state the boss is in

    [Header("The player prefab and transform are already set.  For reference only")]
    public GameObject playerPrefab;//the player prefab goes here and is set at start
    public Transform playerTransform;//the player transform goes here and is set at start

    [Header("The bosses projectile goes here")]
    public GameObject bossProjectile;//the projectile of the boss goes here

    [Header("The bosses right Spawn point")]
    public Transform bossProjectileRightSpawnPoint; //this is the right projectile spawn point

    [Header("The bosses left spawn point")]
    public Transform bossProjectileLeftSpawnPoint; //this is the left projectile spawn point

    [Header("The time between shots.  Needs to be set")]
    public float startTimeBetweenShots; //this is the time that is set in the inspector for the amount of time between shots

    [Header("For reference only. This is the time that is counting down")]
    public float timeBetweenShots; //this is counting down from the start time between shots

    [Header("for reference only.  This is the distance the player is from the boss")]
    public float distanceFromPlayer; //this displays how far the boss is from the player

    [Header("Set this distance for when the boss will be alerted within a certain distance of the player")]
    public float safeDistance;//this needs to be set in the inspector for how far away the player needs to be before it is attacked by the boss

    [Header("Has the boss been alerted?")]
    public bool bossAlerted = false; //the bool for if the boss is alerted

    [Header("For reference only.  It is the movement of the enemy towards the player")]
    public Vector3 destination; //this is the vector 3 for the position of the enemy in the scene while moving towards the player

    [Header("Is the boss Dead?")]
    public bool bossDead = false; //the bool for if the boss is dead

    [Header("Just for reference only, for the time at the nav point")]
    public float timeAtNavPoint; //the amount of time the boss is at a nav point

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
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Calculating the small boss health in Update");
        bossHealthBar.value = CalculatingBossHealth();
        //Debug.Log("continiously checking the boss state in update");
        BossState();
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
            }
        }
    }

    void Moving()//This is the function for the boss moving around from nav point to nav point
    {
        //Debug.Log("the boss is in the moving function and boss dead bool is set to false");
        bossDead = false;

        if (bossPrefab)
        {
            distanceFromPlayer = Vector3.Distance(bossTransform.position, playerTransform.position);
            //Debug.Log("The boss is moving");
            if (!bossAgent.pathPending && bossAgent.remainingDistance < .5f && !bossDead)
            {
                //Debug.Log("Boss is picking the next nav point while in the moving function");
                PickNextNavPoint();
            }
            else if (distanceFromPlayer <= safeDistance)
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
            if (bossAlerted == true && distanceFromPlayer <= safeDistance)
            {
                if (bossAlerted == true)
                {
                    //Debug.Log("setting the boss alerted bool to true");
                    bossAlerted = true;
                    //Debug.Log("setting the bosses destination to where the player is");
                    destination = playerTransform.position;
                    //Debug.Log("setting the boss agents destination to the destination fo the player");
                    bossAgent.destination = destination;
                    //Debug.Log("Boss is shooting in the chase function");
                    BossShooting();
                }
            }
            //Debug.Log("The boss is in the chase function and has not been alerted");
            if (distanceFromPlayer >= safeDistance && bossAlerted == true)
            {
                //Debug.Log("inside the chase funtion and setting the boss alerted to false");
                bossAlerted = false;
                //Debug.Log("boss is switching to the moving function");
                bossStates = GlobalVariables.BossStates.Moving;
            }
            else if (bossBeingAttacked == true)
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
            if (distanceFromPlayer <= safeDistance)
            {
                //Debug.Log("The boss is being attacked in the attacking functions bool is set to true");
                bossBeingAttacked = true;
                if(bossBeingAttacked == true)
                {
                    BossShooting();
                }
            }

            if (distanceFromPlayer >= safeDistance && bossBeingAttacked == true)
            {
                //Debug.Log("setting the boss being attacked to false in the attacking function");
                bossBeingAttacked = false;
                //Debug.Log("boss is moving from the attacking function to the moving function");
                bossStates = GlobalVariables.BossStates.Moving;
            }
        }
    }

    void BossShooting()//the function for if the boss is shooting at the player
    {
        if(timeBetweenShots <= 0)
        {
            //Debug.Log("Instantiating the projectile from the left spawn point");
            Instantiate(bossProjectile, bossProjectileLeftSpawnPoint.position, bossProjectileLeftSpawnPoint.rotation);
            //Debug.Log("Instantiating the projectile from the right spawn point");
            Instantiate(bossProjectile, bossProjectileRightSpawnPoint.position, bossProjectileRightSpawnPoint.rotation);
            //Debug.Log("restting the the time between shots");
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
        if (collision.gameObject.GetComponent<PlayerDamage>() != null)
        {
            //Debug.Log("if the player is attacking the boss change the bossBeingAttacked bool to true");
            bossBeingAttacked = true;
            if (bossBeingAttacked == true)
            {
                //Debug.Log("subtracting Boss Health from when the player is damaging the boss");
                bossHealth -= collision.gameObject.GetComponent<PlayerDamage>().damage;
                //Debug.Log("inside the if statement for the boss being attacked");
                if (bossHealth <= 0)
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
