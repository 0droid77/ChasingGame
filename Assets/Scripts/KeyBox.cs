using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("player gets key");
            other.GetComponent<Player>().isHaveKey = true;
            Destroy(gameObject);
        }
        
        //新建一个脚本，放在WallLaserCam的triggerbox上（红色半透明的长方体上）
        //脚本结构和本脚本大概一致
        //具体检测成功部份如下：
        /*
            Debug.Log("player hits laser ray");
            Destroy(other.gameobject);
         * 
         */
    }
}
