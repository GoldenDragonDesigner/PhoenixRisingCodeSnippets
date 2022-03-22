using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public float searchCountDown = 1f;

    public int numOfEnemies;

    public int maxNumOfEnemies;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyIsAlive();
    }

    //public void EnemyRemoval()
    //{
    //    if(numOfEnemies > 0 && !EnemyIsAlive())
    //    {
    //        numOfEnemies--;
    //    }
    //}

    public bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0.0f)
        {
            searchCountDown = 1.0f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null && GameObject.FindGameObjectWithTag("SlimeEnemy") == null)
            {
                numOfEnemies--;
                return false;
            }
        }
        return true;
    }
}
