using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrone : MonoBehaviour
{
    //This Script is mostly deactive, as asteroids has a set space for the space ship to move around.
    //I planned to have a larger area initially.
    public Transform wizard;

    void Start()
    {

    }

    void Update()
    {
        if (wizard != null)
        {
            //If wizard exists, follow/lerp towards him.
            Vector3 targetCamera = wizard.position;
            targetCamera.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, targetCamera, 0.03f);
        }
    }
}
