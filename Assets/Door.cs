using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnimator;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("player enters");
            if (other.GetComponent<Player>().isHaveKey)
            {
                Debug.Log("player opens doors");
                doorAnimator.SetTrigger("Open");
            }
            else
            {
                Debug.Log("player needs key to open doors");
            }
        }
    }
}
