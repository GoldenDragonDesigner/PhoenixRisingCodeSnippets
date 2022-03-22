using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:  Benjamin Boese
//Date: 11-6-2019
//Purpose: For having Global Variables to use in any script for the states in the enemy system
[System.Serializable]
public static class GlobalVariables
{
    public enum WaveStates { CountingDown, Waiting, Spawning, Death} //enum for the wave states

    public enum EnemyStates { Idle, Moving, Death, RangeAttack, Chasing} //enum for the enemy states 

    public enum BossStates { FirstAttack, SecondAttack, ThirdAttack, Moving, Idle, Attacking, MoveToPosition, Chase} //enum for the boss states
}
