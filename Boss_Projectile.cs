using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:  Benjamin Boese
//Date: 11-6-2019
//Purpose: For making a boss projectile
public class Boss_Projectile : MonoBehaviour
{
    [Header("Speed of the projectile")]
    public float speed; //This is the speed of the projectile

    private Transform playerPrefab;//this is the transform of the player

    private Vector3 playerIsTarget;//this is the vector 3 of the players position
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Setting the player target at start");
        playerPrefab = GameObject.FindWithTag("Player").transform;
        //Debug.Log("setting the playerisTarget vector3 at start");
        playerIsTarget = new Vector3(playerPrefab.position.x, playerPrefab.position.y, playerPrefab.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("moving the boss projectile in a forward direction");
        transform.position += transform.forward * speed * Time.deltaTime;

        if(transform.position.x == playerIsTarget.x && transform.position.y == playerIsTarget.y && transform.position.z == playerIsTarget.z)
        {
            //Debug.Log("Destroying the projectile if it has hit the player");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) //this function just destroys the projectile if it has hit a player tag, enemy tag, an untagged object, or a wall tag
    {
        //Debug.Log("destroying the projectile if is has hit the player, enemy, wall, or untagged");
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Untagged" || collision.gameObject.tag == "Wall")
        {
            SHeartsControlScript.health -= 1;
            if(collision.gameObject.GetComponent<NewPlayerController>() != null)
            {
                collision.gameObject.GetComponent<NewPlayerController>().HitPlayer();
                Destroy(gameObject, 2f);
            }
        }
    }
}
