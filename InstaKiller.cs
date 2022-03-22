using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:  Benjamin Boese
//Date: 11-5-2019
//Purpose: For making a script for instantly kiling something from the debug menu
public class InstaKiller : MonoBehaviour
{
    public void InstaKill()//the function for destorying the gameobject from the debug menu
    {
        //Debug.Log("wave spawner Dead");
        Destroy(this.gameObject);
    }
}
