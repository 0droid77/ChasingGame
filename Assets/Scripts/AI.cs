using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public GameObject Player;
    
    public GameObject[] wayPointsArray;

    public Transform currentWaypoint;
    
    public int currentWaypointIndex = 0;

    public float distanceToCurrentWaypoint;


    public bool isPatrolling;
    public bool isChasing;

    //public List<GameObject> wayPointsList;
    
    

    // Start is called before the first frame update
    void Start()
    {
        //wayPointsArray = new GameObject[5];
        //currentWaypoint
    }

    private void Update()
    {
        if (isPatrolling)
        {
            distanceToCurrentWaypoint = Vector3.Distance(gameObject.transform.position, currentWaypoint.transform.position);
            if (Vector3.Distance(gameObject.transform.position, currentWaypoint.transform.position) <= 1)
            {
                if (currentWaypointIndex == wayPointsArray.Length - 1)
                {
                    currentWaypointIndex = 0;
                }
                else
                {
                    currentWaypointIndex += 1;
                }
                currentWaypoint = wayPointsArray[currentWaypointIndex].transform;
            }
        
            gameObject.GetComponent<NavMeshAgent>().SetDestination(currentWaypoint.position);
        }

        if (isChasing)
        {
            gameObject.GetComponent<NavMeshAgent>().SetDestination(Player.transform.position);
        }
    }
}
