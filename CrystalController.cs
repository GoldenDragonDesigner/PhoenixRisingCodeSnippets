using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CrystalController : MonoBehaviour
{
    [Header("Ice Panel Goes Here")]
    public GameObject iceCrystalPanel;
    [Header("Stone Panel Goes Here")]
    public GameObject stoneCrystalPanel;
    [Header("Levitation Panel Goese Here")]
    public GameObject levitationCrystalPanel;
    [Header("Immortal Panel Goes Here")]
    public GameObject immortalCrystalPanel;
    [Header("Fire Panel Goes Here")]
    public GameObject fireCrystalPanel;
    [Header("Lightening Panel Goes Here")]
    public GameObject lighteningCrystalPanel;

    public static int iceCrystalAmount;
    public static int stoneCrystalAmount;
    public static int levitationCrystalAmount;
    public static int immortalCrystalAmount;
    public static int fireCrystalAmount;
    public static int lighteningCrystalAmount;

    [Header("Max Amount of Crystals")]
    public int maxIceCrystalAmount = 1;
    public int maxStoneCrystalAmount = 1;
    public int maxLevitationCrystalAmount = 1;
    public int maxImmortalCrystalAmount = 1;
    public int maxFireCrystalAmount = 1;
    public int maxLighteningCrystalAmount = 1;

    [Header("Was the crystal Picked Up?")]
    public bool iceCrystalPickedUp = false;
    public bool stoneCrystalPickedUp = false;
    public bool levitationCrystalPickedUp = false;
    public bool immortalCrystalPickedUp = false;
    public bool fireCrystalPickedUp = false;
    public bool lighteningCrystalPickedUp = false;

    public GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        iceCrystalAmount = 0;
        stoneCrystalAmount =0;
        levitationCrystalAmount =0;
        immortalCrystalAmount =0;
        fireCrystalAmount =0;
        lighteningCrystalAmount =0;

        iceCrystalPanel.SetActive(false);
        stoneCrystalPanel.SetActive(false);
        levitationCrystalPanel.SetActive(false);
        immortalCrystalPanel.SetActive(false);
        fireCrystalPanel.SetActive(false);
        lighteningCrystalPanel.SetActive(false);

        iceCrystalPickedUp = false;
        stoneCrystalPickedUp = false;
        levitationCrystalPickedUp = false;
        immortalCrystalPickedUp = false;
        fireCrystalPickedUp = false;
        lighteningCrystalPickedUp = false;
    }

    private void Update()
    {
       //IceCrystalCollected();
        //IceCrystalUsed();

        //StoneCrystalCollected();
        //StoneCrystalUsed();

        //LevitationCrystalCollected();
        //LevitationCrystalUsed();

        //ImmortalCrystalCollected();
        //ImmortalCrystalUsed();

        //FireCrystalCollected();
        //FireCrystalUsed();

        //LighteningCrystalCollected();
        //LighteningCrystalUsed();
    }

    public void IceCrystalCollected()
    {
        if(iceCrystalAmount > maxIceCrystalAmount)
        {
            iceCrystalAmount = 1;
            iceCrystalPickedUp = true;
            iceCrystalPanel.SetActive(true);
            Debug.Log("IC Collected");
        }
    }

    //public void IceCrystalUsed()
    //{
    //    if(iceCrystalAmount == maxIceCrystalAmount)
    //    {
    //        iceCrystalAmount = 0;
    //        iceCrystalPickedUp = false;
    //        iceCrystalPanel.SetActive(false);
    //        Debug.Log("IC Used");
    //    }
    //}

    public void StoneCrystalCollected()
    {
        if(stoneCrystalAmount > maxStoneCrystalAmount)
        {
            stoneCrystalAmount = 1;
            stoneCrystalPickedUp = true;
            stoneCrystalPanel.SetActive(true);
            Debug.Log("Stone Crystal Collected");
        }
    }

    //public void StoneCrystalUsed()
    //{
    //    if(stoneCrystalAmount == maxStoneCrystalAmount)
    //    {
    //        stoneCrystalAmount = 0;
    //        stoneCrystalPickedUp = false;
    //        stoneCrystalPanel.SetActive(false);
    //        Debug.Log("SC Used");
    //    }
    //}

    public void LevitationCrystalCollected()
    {
        if(levitationCrystalAmount > maxLevitationCrystalAmount)
        {
            levitationCrystalAmount = 1;
            levitationCrystalPickedUp = true;
            levitationCrystalPanel.SetActive(true);
            Debug.Log("Lev Crystal Collected");
        }
    }

    //public void LevitationCrystalUsed()
    //{
    //    if(levitationCrystalAmount == maxLevitationCrystalAmount)
    //    {
    //        levitationCrystalAmount = 0;
    //        levitationCrystalPickedUp = false;
    //        levitationCrystalPanel.SetActive(false);
    //        Debug.Log("LC Used");
    //    }
    //}

    public void ImmortalCrystalCollected()
    {
        if(immortalCrystalAmount > maxImmortalCrystalAmount)
        {
            immortalCrystalAmount = 1;
            immortalCrystalPickedUp = true;
            immortalCrystalPanel.SetActive(true);
            Debug.Log("Immortal Crystal Collected");
        }
    }

    //public void ImmortalCrystalUsed()
    //{
    //    if(immortalCrystalAmount == maxImmortalCrystalAmount)
    //    {
    //        immortalCrystalAmount = 0;
    //        immortalCrystalPickedUp = false;
    //        immortalCrystalPanel.SetActive(false);
    //        Debug.Log("IC Used");
    //    }
    //}

    public void FireCrystalCollected()
    {
        if(fireCrystalAmount > maxFireCrystalAmount)
        {
            fireCrystalAmount = 1;
            fireCrystalPickedUp = true;
            fireCrystalPanel.SetActive(true);
            Debug.Log("Fire Crystal Collected");
        }

    }

    //public void FireCrystalUsed()
    //{
    //    if(fireCrystalAmount == maxFireCrystalAmount)
    //    {
    //        fireCrystalAmount = 0;
    //        fireCrystalPickedUp = false;
    //        fireCrystalPanel.SetActive(false);
    //        Debug.Log("FC Used");
    //    }
    //}

    public void LighteningCrystalCollected()
    {
        if(lighteningCrystalAmount > maxLighteningCrystalAmount)
        {
            lighteningCrystalAmount = 1;
            lighteningCrystalPickedUp = true;
            lighteningCrystalPanel.SetActive(true);
            Debug.Log("Lightening Crystal Collected");
        }
    }

    //public void LighteningCrystalUsed()
    //{
    //    if(lighteningCrystalAmount == maxLighteningCrystalAmount)
    //    {
    //        lighteningCrystalAmount = 0;
    //        lighteningCrystalPickedUp = false;
    //        lighteningCrystalPanel.SetActive(false);
    //        Debug.Log("LC Used");
    //    }
    //}
}
