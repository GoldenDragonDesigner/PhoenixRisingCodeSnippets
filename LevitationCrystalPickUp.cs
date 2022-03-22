using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitationCrystalPickUp : MonoBehaviour
{
    public GameObject crystalController;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && CrystalController.levitationCrystalAmount == 0)
        {
            CrystalController.levitationCrystalAmount += 1;
            crystalController.gameObject.GetComponent<CrystalController>().LevitationCrystalCollected();
            PickUp();
            Debug.Log("Levitation Crystal Picked UP");
        }
    }

    void PickUp()
    {
        Destroy(gameObject);
    }
}
