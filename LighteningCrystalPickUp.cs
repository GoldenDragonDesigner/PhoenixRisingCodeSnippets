using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighteningCrystalPickUp : MonoBehaviour
{
    public GameObject crystalController;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && CrystalController.lighteningCrystalAmount == 0)
        {
            CrystalController.lighteningCrystalAmount += 1;
            crystalController.gameObject.GetComponent<CrystalController>().LighteningCrystalCollected();
            PickUp();
            Debug.Log("Lightening Crystal Picked UP");
        }
    }

    void PickUp()
    {
        Destroy(gameObject);
    }
}
