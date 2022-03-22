using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:  Benjamin Boese
//Date: 10-29-219
//contributor: Enemy Shoot, Run and Hide Tutorial. (2016, January 12). Retrieved October 29, 2019, from https://www.youtube.com/watch?v=-luoHNdeAu0&t=262s.
//purpose:  To make an enemy move around the area
public class EnemyPatrol : MonoBehaviour
{
    [Header("The walking force of the enemy")]
    public float walkMoveForce = 0f;
    [Header("The running force of the enemy")]
    public float runMoveForce = 0f;
    [Header("The move force of the enemy")]
    public float moveForce = 0f;
    [Header("What is the player's Layer?")]
    public LayerMask whatIsPlayer;
    [Header("What is the obstacles Layer?")]
    public LayerMask whatIsObstacle;
    [Header("The check for distance from an object")]
    public float wallCheckDist = 0f;
    [Header("The shooting Rate")]
    public float shootRate = 0f;
    [Header("The player prefab goes here")]
    public GameObject playerPrefab;
    [Header("The distance from the player")]
    public float distanceFromTarget = 0f;
    [Header("The distance that is safe from the player")]
    public float safeDistance = 0f;
    [Header("Running Turn rate")]
    public float runTurnRate = 0f;
    [Header("Turning Distance Check")]
    public float runTurnDistCheck = 0f;

    private bool shotFired = false;
    private float shootTimeStamp = 0f;
    private float runTurnTimeStamp = 0f;
    private Vector3 moveDir;
    private Rigidbody rb;
    private bool targetFound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveDir = ChooseDirection();
        transform.rotation = Quaternion.LookRotation(moveDir);
        moveForce = walkMoveForce;
        runTurnRate = Random.Range(.5f, 1.5f);
    }

    void Update()
    {
        if (targetFound)
        {
            Shoot();
        }
        else
        {
            LookForTarget();
        }

        distanceFromTarget = Vector3.Distance(transform.position, playerPrefab.transform.position);
    }

    void Shoot()
    {
        if (!shotFired)
        {
            if(Time.time > shootTimeStamp)
            {
                SHeartsControlScript.health -= 1;
                moveForce = runMoveForce;
                shotFired = true;
                moveDir = ChooseDirection();
                transform.rotation = Quaternion.LookRotation(moveDir);
                shootTimeStamp = Time.time + shootRate;
            }
            else
            {
                Hide();
            }

        }
    }

    void Hide()
    {
        if(distanceFromTarget < safeDistance)
        {
            RunToHide();
        }
        else
        {
            moveForce = walkMoveForce;
            shotFired = false;
            targetFound = false;
            moveDir = ChooseDirection();
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
    }

    void RunToHide()
    {
        rb.velocity = moveDir * moveForce;
        
        if(Time.time > runTurnTimeStamp)
        {
            if (Physics.Raycast(transform.position, transform.right, runTurnDistCheck, whatIsObstacle))
            {
                moveDir = -transform.right;
                transform.rotation = Quaternion.LookRotation(moveDir);
            }
            else if(Physics.Raycast(transform.position, -transform.right, runTurnDistCheck, whatIsObstacle))
            {
                moveDir = transform.right;
                transform.rotation = Quaternion.LookRotation(moveDir);
            }
            else
            {
                moveDir = ChooseDirectionLR();
                transform.rotation = Quaternion.LookRotation(moveDir);
            }
            runTurnRate = Random.Range(0.5f, 1.5f);
            runTurnTimeStamp = Time.time + runTurnRate;
        }
    }

    Vector3 ChooseDirectionLR()
    {
        System.Random ran = new System.Random();
        int i = ran.Next(0, 1);
        Vector3 temp = new Vector3();

        if(i == 0)
        {
            temp = transform.right;
        }
        else if(i == 1)
        {
            temp = -transform.right;
        }
        return temp;
    }

    Vector3 ChooseDirection()
    {
        System.Random ran = new System.Random();
        int i = ran.Next(0, 3);
        Vector3 temp = new Vector3();

        if(i == 0)
        {
            temp = transform.forward;
        }
        else if(i == 1)
        {
            temp = -transform.forward;
        }
        else if(i == 2)
        {
            temp = transform.right;
        }
        else if(i == 3)
        {
            temp = -transform.right;
        }
        return temp;
    }

    void LookForTarget()
    {
        Move();
        targetFound = Physics.Raycast(transform.position, transform.forward, Mathf.Infinity, whatIsPlayer);
    }

    void Move()
    {
        rb.velocity = moveDir * moveForce;

        if(Physics.Raycast(transform.position, transform.forward, wallCheckDist, whatIsObstacle))
        {
            moveDir = ChooseDirection();
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
    }

}
