using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicMissile : MonoBehaviour
{
    Scene currentScene;
    public GameObject magicMissilePrefab;
    float cooldownTimer = 0.0f;
    public float castDelay = 0.5f;
    public AudioClip cast;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (Input.GetButton("Fire1") && cooldownTimer <= 0)
        {
            //If Player holds M1, fire.
            AudioSource.PlayClipAtPoint(cast, gameObject.transform.position);
            //Debug.Log("Magic Missile!");
            cooldownTimer = castDelay;
            Instantiate(magicMissilePrefab, transform.position, transform.rotation);
            
        }
        if (Input.GetButton("Fire2") && cooldownTimer <= 0 && currentScene.name == "SpecialScene")
        {
            //If Player holds M2, triple cast.
            AudioSource.PlayClipAtPoint(cast, gameObject.transform.position);
            //Debug.Log("Triple Magic Missile!");
            cooldownTimer = castDelay + 1.0f;
            Vector3 leftMissile = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.1f, transform.position.z);
            Vector3 rightMissile = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.1f, transform.position.z);
            Instantiate(magicMissilePrefab, leftMissile, transform.rotation); //Left
            Instantiate(magicMissilePrefab, rightMissile, transform.rotation); //Right
            Instantiate(magicMissilePrefab, transform.position, transform.rotation); //Normal
        }
    }
}
