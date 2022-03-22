using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Author:  Benjamin Boese
//Date: 11-6-2019
//Purpose: For making a scene loader that loads with a string name
public class SceneLoader : MonoBehaviour
{
    [Header("Type Scene Name Here(Is case Sensitive)")]
    [SerializeField]
    private string sceneChanger; //This is the string variable 

    public void ChangeScenes()
    {
        SceneManager.LoadScene(sceneChanger);
    }
}
