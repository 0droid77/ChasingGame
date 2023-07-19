using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFan : MonoBehaviour
{
    public AI ai;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Detect Player, Enter Chase Mode");
            ai.isChasing = true;
            ai.isPatrolling = false;
        }
    }
}
