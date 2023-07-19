using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLaserCam : MonoBehaviour
{
    public Transform laserPoint;
    public float distanceFromOriginToHitPoint;

    private Rigidbody myRB;

    public float moveSpeed;
    public Transform leftEnd;
    public Transform rightEnd;

    public Vector3 moveDir;

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myRB.velocity = moveDir.normalized * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        //int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        if (gameObject.transform.localPosition.z >= leftEnd.localPosition.z)
        {
            myRB.velocity = moveDir.normalized * moveSpeed;
        }

        if (gameObject.transform.localPosition.z <= rightEnd.localPosition.z)
        {
            myRB.velocity = moveDir.normalized * -moveSpeed;
        }
        
        RaycastHit hit;
        
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(laserPoint.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                Debug.DrawRay(laserPoint.position, transform.forward * hit.distance, Color.yellow);
                Debug.Log("Hit Wall");
                distanceFromOriginToHitPoint = hit.distance;
            }
            else
            {
                Debug.DrawRay(laserPoint.position, transform.forward * 1000, Color.white);
                distanceFromOriginToHitPoint = hit.distance;
            }

            Vector3 tempScale = laserPoint.localScale;
            tempScale.z = distanceFromOriginToHitPoint;
            laserPoint.localScale = tempScale;
        }
    }
}
