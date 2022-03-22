using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCrystalPickUp : MonoBehaviour
{
    public GameObject crystalController;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && CrystalController.fireCrystalAmount == 0)
        {
            CrystalController.fireCrystalAmount += 1;
            crystalController.GetComponent<CrystalController>().FireCrystalCollected();
            PickUp();
            Debug.Log("Fire Crystal Picked UP");
        }
    }

    void PickUp()
    {
        Destroy(gameObject);
    }
}
