using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMissile : MonoBehaviour
{
    public float timer = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If the object exists for 5 seconds (unusual if the missile will be on screen for longer) delete it.
        timer = timer - Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
