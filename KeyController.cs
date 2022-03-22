using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyController : MonoBehaviour
{
    public GameObject keyPanel;
    public GameObject player;
    public static int keyAmount;
    public int maxKeyAmount = 1;
    public bool keyPickedUp = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        keyAmount = 0;
        keyPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        KeyCollection();
        KeyUsed();
    }

    public void KeyCollection()
    {
        if(keyAmount > maxKeyAmount)
        {
            keyAmount = 1;
            keyPickedUp = true;
            keyPanel.gameObject.SetActive(true);
        }
    }

    public void KeyUsed()
    {
        if (keyAmount == maxKeyAmount)
        {
            keyAmount = 0;
            keyPickedUp = false;
            keyPanel.gameObject.SetActive(false);
        }
    }
}
