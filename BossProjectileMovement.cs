using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:  Benjamin Boese
//Date: 11-6-2019
//Purpose: For making a boss projectile movement move in 3d space
public class BossProjectileMovement : MonoBehaviour
{
    [Header("How fast do you want the projectile to go?")]
    public float speed; //this is the float for the speed of the projectile

    [Header("This is the transform of the player")]
    public Transform player; //this is the transform of the player 

    [Header("This is the vector 3 of the players position")]
    public Vector3 playerTarget; //this is the vector 3 of the player position

    [Header("The projectile prefab goes here")]
    public GameObject bullets; //this is the prefab for the projectile that is being moved

    [Header("the audio sound for the projectile movement")]
    public AudioSource Shootingsound;//this is the sound for the projectile

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;//setting the player's transform at start by looking for the tag
        playerTarget = new Vector3(player.position.x, player.position.y, player.position.z);//setting the player target's vector 3 at start
    }

    // Update is called once per frame
    void Update()//updaing the boss projectile position and destroying it if it hits the player
    {
        if (transform.position.x == playerTarget.x && transform.position.y == playerTarget.y && transform.position.z == playerTarget.z)
        {
            DestoryProjectile();
        }

    }

    void DestoryProjectile()//destorying the projectile game oject function
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)//the function that if the projectile has collided with something that it gets destroyed
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Untagged" || collision.gameObject.tag == "Wall")
        {
            DestoryProjectile();
        }
    }

    void Shooting()//the function for the shooting sound of the projectile
    {
        Shootingsound.Play(0);
    }
}
