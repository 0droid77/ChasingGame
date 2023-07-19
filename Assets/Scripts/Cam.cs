using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Light camSpotLight;
    public Light camPointLight;
    public Material camHeadMat;

    public Color camSafeColor;
    public Color camDetectColor;

    public bool isDetectPlayer;

    public float camRotateSpeed = .1f;
    public float camRotateLimitation = 60f;
    public GameObject cam;

    public float camAngle;
    public float timer;

    public GameObject player;

    private float attack;

    //public Animator camAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        SetCamStatue();

        RotateCam();
        
        //cam.transform.LookAt(player.transform);

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<Player>().isInSafeZone == false)
            {
                Debug.Log("detect player");
                isDetectPlayer = true;
                
            }
            else
            {
                Debug.Log("player in safe zone");
                isDetectPlayer = false;
            }
        }
        else
        {
            Debug.Log("no player");
            isDetectPlayer = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player exit detection");
            isDetectPlayer = false;
        }
    }

    public void SetCamStatue()
    {
        if (isDetectPlayer)
        {
            camSpotLight.color = camDetectColor;
            camPointLight.color = camDetectColor;
            camHeadMat.SetColor("_Color",camDetectColor);
            camHeadMat.SetColor("_EmissionColor",camDetectColor);
            //camAnimator.SetBool("IsDetectPlayer",true);
            //camAnimator.speed = 0;
        }
        else
        {
            camSpotLight.color = camSafeColor;
            camPointLight.color = camSafeColor;
            camHeadMat.SetColor("_Color",camSafeColor);
            camHeadMat.SetColor("_EmissionColor",camSafeColor);
            //camAnimator.SetBool("IsDetectPlayer",false);
            //camAnimator.speed = 1;
        }
    }

    public void RotateCam()
    {
        if (!isDetectPlayer)
        {
            camAngle += camRotateSpeed * Time.deltaTime;

            if (camAngle > camRotateLimitation || camAngle < -camRotateLimitation)
            {
                camRotateSpeed *= -1;
                //camAngle -= camRotateLimitation + camRotateLimitation;
            }
            cam.transform.rotation = Quaternion.Euler(0f, camAngle, 0f);
        }
        else
        {
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            cam.transform.rotation = Quaternion.Euler(0f, angle + 90f, 0f);
        }
    }
}
