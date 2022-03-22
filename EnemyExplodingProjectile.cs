using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplodingProjectile : MonoBehaviour
{
    [Header("The speed of the projectile")]
    public float speed;

    [Header("Put the rigidbody component checked as Is Kinematic")]
    public Rigidbody rb;

    [Header("For reference only. This is the stored velocity")]
    public Vector3 storedVelocity;

    [Header("For reference only. This is set at start")]
    public Transform player;

    [Header("For reference only.  This is set at start")]
    public Vector3 target;

    [Header("damage Game Object goes here")]
    public GameObject blastRadius;
    //public GameObject blastRadiusPFI;

    public GameObject explosionFx;
    public Transform deathLocation;

    public bool hasHit;


    void Start()
    {
        //Debug.Log("Setting the Player transform at start");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Debug.Log("Setting the Player as target at start");
        target = new Vector3(player.position.x, player.position.y, player.position.z);
        //Debug.Log("Setting the rb velocity to rb.veloctiy * speed");
        rb.velocity = rb.velocity * speed;
        //Debug.Log("Blast raduis damage volume is set inactive");
        blastRadius.SetActive(false);
        //Debug.Log("blast Radius setactive false at start");
        //blastRadiusPFI.SetActive(false);
        //Debug.Log("Setting the has hit bool to false at start");
        hasHit = false;
    }

    void Update()
    {
        //Debug.Log("Always setting the stored velocity in update");
        storedVelocity = rb.velocity;
        //Debug.Log("moving the enemy projectile towards the vector 3 position");
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Debug.Log("Projectile has hit the wall setting the has hit to true");
            hasHit = true;
            DestroyProjectile();
        }
        if(collision.gameObject.tag == "Player")
        {
            hasHit = true;
            DestroyProjectile();
        }
        if(collision.gameObject.tag == "ground")
        {
            hasHit = true;
            DestroyProjectile();
        }
        if(collision.gameObject.tag == "Forcefield")
        {
            hasHit = true;
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        if(hasHit == true)
        {
            Debug.Log("blast radius is active");
            blastRadius.SetActive(true);
            //if (blastRadius)
            //{
                //Debug.Log("blast radius PFI is active");
                //blastRadiusPFI.SetActive(true);
                //Debug.Log("instantiating the explosion effect");
                GameObject explodeFX = (GameObject)Instantiate(explosionFx, deathLocation.transform.position, deathLocation.transform.rotation);
                Destroy(explodeFX, 1f);
            //}
            Debug.Log("Destorying the game object");
            Destroy(gameObject, .2f);        
        }
    }

}
