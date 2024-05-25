using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSc : MonoBehaviour
{
    Vector3 target;
    bool move=false;
    public void CameraMove(Vector3 targets)
    {
        target = targets;
        move = true;
    }
    void FixedUpdate()
    {
       
        
         if (move && transform.position!=target)
        {
            transform.position = Vector3.Slerp(transform.position, target, 5f * Time.deltaTime);

        }
        else
        {
            move = false;
        }
        
    }
}
