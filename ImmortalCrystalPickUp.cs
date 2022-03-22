using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalCrystalPickUp : MonoBehaviour
{
    public GameObject crystalController;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && CrystalController.immortalCrystalAmount == 0)
        {
            CrystalController.immortalCrystalAmount += 1;
            crystalController.gameObject.GetComponent<CrystalController>().ImmortalCrystalCollected();
            PickUp();
            Debug.Log("Immortal Crystal Picked UP");
        }
    }

    void PickUp()
    {
        Destroy(gameObject);
    }
}
