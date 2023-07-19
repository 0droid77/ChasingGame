using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
#if false // Obsolete
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

    [Header("References")]
    [SerializeField] private Transform _PlayerCameraHolder;
    [SerializeField] private Camera _PlayerCamera;

    [Header("Objects")]
    public bool IsHaveKey;
    public bool IsInSafeZone;

    [Header("Rotation")]
    [SerializeField] private float _RotationDuration;
    [SerializeField, Min(0)] private int _RtationDegree;
    private bool _IsRotating = false;
    private float _RotationTimer;
    private Vector3 _StartEulerAngle;
    private Vector3 _EndEulerAngle;

    [Header("Movement")]
    [SerializeField] private float _MoveSpeed;

    private void Update()
    {
        Rotation();
        Movement();
    }

    /// <summary>
    /// Rotate 90 degree per trigger.
    /// </summary>
    private void Rotation()
    {
        if (_IsRotating) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _IsRotating = true;

            _RotationTimer = 0;

            _StartEulerAngle = _PlayerCameraHolder.localEulerAngles;
            _EndEulerAngle = _PlayerCameraHolder.localEulerAngles - new Vector3(0, _RtationDegree, 0);

            StartCoroutine(IERotatePlayer());
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _IsRotating = true;

            _RotationTimer = 0;

            _StartEulerAngle = _PlayerCameraHolder.localEulerAngles;
            _EndEulerAngle = _PlayerCameraHolder.localEulerAngles + new Vector3(0, _RtationDegree, 0);

            StartCoroutine(IERotatePlayer());
        }
    }

    /// <summary>
    /// Move toward camera forward.
    /// </summary>
    private void Movement()
    {
        Vector3 _MoveDirection = Vector3.zero;
        Vector3 _CameraRelatedForward = new Vector3(_PlayerCamera.transform.forward.x, 0, _PlayerCamera.transform.forward.z).normalized;
        Vector3 _CameraRelatedRight = new Vector3(_PlayerCamera.transform.right.x, 0, _PlayerCamera.transform.right.z).normalized;

        if (Input.GetKey(KeyCode.W))
        {
            _MoveDirection += _CameraRelatedForward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _MoveDirection -= _CameraRelatedForward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _MoveDirection -= _CameraRelatedRight;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _MoveDirection += _CameraRelatedRight;
        }

        transform.Translate(_MoveDirection * _MoveSpeed * Time.deltaTime);
    }

    private IEnumerator IERotatePlayer()
    {
        if (_RotationTimer >= _RotationDuration)
        {
            _IsRotating = false;
            yield break;
        }

        _RotationTimer += Time.deltaTime;
        _PlayerCameraHolder.localEulerAngles = Vector3.Lerp(_StartEulerAngle, _EndEulerAngle, _RotationTimer / _RotationDuration);

        yield return new WaitForEndOfFrame();

        StartCoroutine(IERotatePlayer());
    }

    private IEnumerator IEMovePlayer()
    {
        yield break;
    }
}
