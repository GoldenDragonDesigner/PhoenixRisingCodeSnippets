using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCrystalPickUp : MonoBehaviour
{
    public GameObject crystalController;
    public GameObject playerGun;
    public GameObject ammoPanel;
    public bool playersGun;

    public void Start()
    {
       ammoPanel.SetActive(false);
        playersGun = false;
       playerGun.GetComponent<Shooting>().enabled = false;
        print("Player gun is false");
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(playersGun == false)
            {
                playersGun = true;
                print("Gun active");
                playerGun.SetActive(true);
                print("player can use gun because they now have the ice stone");
                ammoPanel.SetActive(true);
            }
            else
            {
                playersGun = false;
            }
        }
        if (other.tag == "Player" && CrystalController.iceCrystalAmount == 0)
        {
            playerGun.GetComponent<Shooting>().enabled = true;
            //ammoPanel.SetActive(true);
            CrystalController.iceCrystalAmount += 1;
            crystalController.GetComponent<CrystalController>().IceCrystalCollected();           
            Debug.Log("Ice Crystal Picked UP");
            PickUp();
        }
        else
        {
            CrystalController.iceCrystalAmount = 0;
        }

    }
    public void IceCrystal()
    {
        playerGun.GetComponent<Shooting>().enabled = true;
        ammoPanel.SetActive(true);
        CrystalController.iceCrystalAmount += 1;
        crystalController.GetComponent<CrystalController>().IceCrystalCollected();
        Debug.Log("Ice Crystal Picked UP");
        PickUp();
        Debug.Log("Fire at Will");
    }
    void PickUp()
    {
        Destroy(gameObject);
    }
}
