using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrone : MonoBehaviour
{
    public Transform wizard;

    void Start()
    {

    }

    void Update()
    {
        if (wizard != null)
        {
            Vector3 targetCamera = wizard.position;
            targetCamera.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, targetCamera, 0.03f);
        }
    }
}
