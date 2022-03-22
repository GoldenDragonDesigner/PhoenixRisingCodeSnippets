using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUsed : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
                if (Input.GetKeyDown(KeyCode.E) && KeyController.keyAmount != 0)
                {
                    KeyController.keyAmount -= 1;
                }
        }
    }
}
