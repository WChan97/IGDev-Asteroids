using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : MonoBehaviour
{
    public GameObject magicMissilePrefab;
    float cooldownTimer = 0.0f;
    public float castDelay = 0.5f;
    public AudioClip cast;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (Input.GetButton("Fire1") && cooldownTimer <= 0)
        {
            //If Player holds M1, fire.
            AudioSource.PlayClipAtPoint(cast, gameObject.transform.position);
            Debug.Log("Magic Missile!");
            cooldownTimer = castDelay;
            Instantiate(magicMissilePrefab, transform.position, transform.rotation);
            
        }
    }
}
