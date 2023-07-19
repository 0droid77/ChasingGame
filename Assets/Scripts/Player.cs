using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
#if false
    public bool isInSafeZone;

    public float cameraRotationSpeed = 90f; // The speed at which the camera rotates
    public float cameraRotationDuration = 2f; // The time it takes to complete the camera rotation
    private Transform cameraTransform; // The Transform component of the camera
    private Quaternion targetRotation; // The target rotation for the camera
    private float rotationStartTime; // The time the camera rotation started
    public float playerMovementSpeed = 5f;
    
    private bool isRotating = false; // Whether the camera is currently rotating
    private float gameTime;

    public float PlayerMovingDirection_WS = 0;
    public float PlayerMovingDirection_AD = 90;

    public int directionIndex = 1;

    public bool isHaveKey;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SafeZone")
        {
            isInSafeZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SafeZone")
        {
            isInSafeZone = false;
        }
    }
    


    private void Start()
    {
        // Find the Transform component of the camera
        if (transform.GetChild(0) != null)
        {
            cameraTransform = transform.GetChild(0);
        }
    }

    private void Update()
    {
        gameTime = Time.time;
        if (!isRotating)
        {
            // Check if the Q key is pressed
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // Set the target rotation to rotate 90 degrees to the left
                targetRotation = Quaternion.Euler(0f, -cameraRotationSpeed, 0f) * cameraTransform.rotation;

                // Set the rotation start time
                rotationStartTime = Time.time;

                // Set isRotating to true
                isRotating = true;

                if (directionIndex == 4)
                {
                    directionIndex = 1;
                }
                else
                {
                    directionIndex++;
                }

                SwitchMovingAxis(directionIndex);
            }

            // Check if the E key is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Set the target rotation to rotate 90 degrees to the right
                targetRotation = Quaternion.Euler(0f, cameraRotationSpeed, 0f) * cameraTransform.rotation;

                // Set the rotation start time
                rotationStartTime = Time.time;

                // Set isRotating to true
                isRotating = true;
                
                if (directionIndex == 1)
                {
                    directionIndex = 4;
                }
                else
                {
                    directionIndex--;
                }
                
                SwitchMovingAxis(directionIndex);
            }
            
            if (Input.GetKey(KeyCode.W))
            {
                // Calculate the forward direction of the camera
                Vector3 cameraForward = cameraTransform.forward;

                // Remove the Y component of the forward direction to prevent flying and normalize it
                cameraForward.y = 0;
                cameraForward.z = PlayerMovingDirection_AD;
                cameraForward.x = PlayerMovingDirection_WS;
                cameraForward.Normalize();

                // Move the player in the direction where the camera is facing
                transform.position += cameraForward * playerMovementSpeed * Time.deltaTime;
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                // Calculate the forward direction of the camera
                Vector3 cameraForward = cameraTransform.forward;

                // Remove the Y component of the forward direction to prevent flying and normalize it
                cameraForward.y = 0;
                cameraForward.z = PlayerMovingDirection_AD;
                cameraForward.x = PlayerMovingDirection_WS;
                cameraForward.Normalize();

                // Move the player in the direction where the camera is facing
                transform.position -= cameraForward * playerMovementSpeed * Time.deltaTime;
            }
        }

        // Check if the camera is still rotating
        if (isRotating && Time.time < rotationStartTime + (cameraRotationDuration - 1.5f))
        {
            // Interpolate the camera rotation between the current and target rotations over time
            float rotationProgress = (Time.time - rotationStartTime) / cameraRotationDuration;
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, targetRotation, rotationProgress);
        }
        else
        {
            // Set isRotating to false
            isRotating = false;
        }
    }

    public void SwitchMovingAxis(int directionIndex)
    {
        if (directionIndex == 1)
        {
            PlayerMovingDirection_WS = 0;
            PlayerMovingDirection_AD = 90;
        }
        if (directionIndex == 2)
        {
            PlayerMovingDirection_WS = -90;
            PlayerMovingDirection_AD = 0;
        }
        if (directionIndex == 3)
        {
            PlayerMovingDirection_WS = 0;
            PlayerMovingDirection_AD = -90;
        }
        if (directionIndex == 4)
        {
            PlayerMovingDirection_WS = 90;
            PlayerMovingDirection_AD = 0;
        }
    }
#endif
    public bool isHaveKey;
    public bool isInSafeZone;

    private void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

        }
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
    }
}
