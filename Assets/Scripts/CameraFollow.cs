using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    [SerializeField] GameObject followTarget;
    IEnumerator Start()
    {
        while(true){
            //camera follow the player
            transform.position = new Vector3(followTarget.transform.position.x + 9f, 0f, -10f);
            yield return null;
        }
    }
}
