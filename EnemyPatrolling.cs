using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:  Benjamin Boese
//Date: 11-6-2019
//Purpose: For making a boss
public class EnemyPatrolling : BaseEnemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        base.playerPrefab = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyState()//this is the function being called from the base enemy for what state the enemy is in right now
    {
        //Debug.Log("enemy patrolling is in the enemy state");
        base.EnemyState();
    }

    protected override float CalculatingEnemyHealth()//this is the function being called from the base enemy for calculating the enemies health
    {
        return base.CalculatingEnemyHealth();
    }

    protected override void OnCollisionEnter(Collision collision)//this is the function being called from the base enemy for if the enemy is being attacked by the player
    {
        base.OnCollisionEnter(collision);
    }

    public void InstaKill() //this is the function for killing the enemy from the debug menu
    {
        Destroy(this.gameObject);
        //Debug.Log("Enemy Patrolling Instakill");
    }
}
