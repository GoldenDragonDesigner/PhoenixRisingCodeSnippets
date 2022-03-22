using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCrystalPickUp : MonoBehaviour
{

    public MeleeWeapon Attack;
    public bool SwingingMelee;
    public GameObject crystalController;
    public GameObject weapon;

    void start()
    {
        SwingingMelee = false;
        print("swinging melee is false");
        
    }

    public void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {
            if (SwingingMelee == false)
            {
                
                SwingingMelee = true;
                print("Got It");
                weapon.SetActive(true);
                Attack.AttackHim();
                print("player can use melee becuase they have the earth stone");
            }
     
        }
        else
        {
            SwingingMelee = false;
            print("player can not use melee because they dont have earth stone");

        }
        if (other.tag == "Player" && CrystalController.stoneCrystalAmount == 0)
        {
            CrystalController.stoneCrystalAmount += 1;
            crystalController.GetComponent<CrystalController>().StoneCrystalCollected();
            PickUp();
            Debug.Log("Stone Crystal Picked UP");         
        }
        else
        {
            CrystalController.stoneCrystalAmount = 0;
            //crystalController.GetComponent<CrystalController>().StoneCrystalCollected();
        }
    }
    public void StoneCrystal()
    {
        CrystalController.stoneCrystalAmount += 1;
        crystalController.GetComponent<CrystalController>().StoneCrystalCollected();
        PickUp();
        SwingingMelee = true;
        print("Got It");
        weapon.SetActive(true);
        Attack.AttackHim();
        Debug.Log("Attack");
    }
    void PickUp()
    {
        Destroy(gameObject);
    }
}
