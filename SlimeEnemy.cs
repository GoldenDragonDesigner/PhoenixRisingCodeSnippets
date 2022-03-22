using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//Author:  Benjamin Boese
//Date: 11-7-2019
//Purpose: For making a slime enemy derived from the base enemy
public class SlimeEnemy : BaseEnemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        base.enemyTransform = GameObject.FindWithTag("SlimeEnemy").transform;
        base.enemyPrefab = GameObject.FindWithTag("SlimeEnemy");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnCollisionEnter(Collision collision)//this function is being called from the base enemy script for when the player is damaging the enemy
    {
        base.OnCollisionEnter(collision);
    }

    public void InstaKill() //this is the function for killing the enemy instantly from the debug menu
    {
        Destroy(this.gameObject);
    }
}