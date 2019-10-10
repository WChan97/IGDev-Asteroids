﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTravel : MonoBehaviour
{
    float maxSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);
        pos += transform.rotation * velocity;
        transform.position = pos;
    }

    void OnTriggerEnter2D()
    {
        Destroy(gameObject);
    }
}