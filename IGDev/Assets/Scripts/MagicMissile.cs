using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicMissile : MonoBehaviour
{
    Scene currentScene;
    public GameObject magicMissilePrefab;
    float cooldownTimer = 0.0f;
    float castDelay = 0.3f;
    public AudioClip cast;
    float hasteCooldown = 3.0f;
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
            AudioSource.PlayClipAtPoint(cast, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10.0f));
            //Debug.Log("Magic Missile!");
            cooldownTimer = castDelay;
            Instantiate(magicMissilePrefab, transform.position, transform.rotation);
            
        }
        if (Input.GetButton("Fire2") && cooldownTimer <= 0 && currentScene.name == "SpecialScene")
        {
            //If Player holds M2, triple cast.
            AudioSource.PlayClipAtPoint(cast, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10.0f));
            //Debug.Log("Triple Magic Missile!");
            cooldownTimer = castDelay + 0.3f;
            Vector3 leftMissile = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.1f, transform.position.z);
            Vector3 rightMissile = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.1f, transform.position.z);
            Instantiate(magicMissilePrefab, leftMissile, transform.rotation); //Left
            Instantiate(magicMissilePrefab, rightMissile, transform.rotation); //Right
            Instantiate(magicMissilePrefab, transform.position, transform.rotation); //Normal
        }

        if (currentScene.name == "SpecialScene")
        {
            if (Input.GetKeyDown("e") == true && hasteCooldown <= 0.0f)
            {
                castDelay = 0.1f;
                hasteCooldown = 3.0f;
            }
            hasteCooldown -= Time.deltaTime;
            if (hasteCooldown <= 1.5f)
            {
                castDelay = 0.3f;
            }
        }
    }
}
