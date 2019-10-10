using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    //Player Respawn
    public GameObject wizardPrefab;
    GameObject playerInstance;
    public int numLives = 4; //4, the player spawns, consuming the first.
    float respawnTimer;
    //Enemy Respawn
    public GameObject SlimeSmallPrefab;
    public GameObject SlimeLargePrefab;
    float rateOfSpawn = 5.0f;
    float spawnDistance = 12f;
    float nextEnemy = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInstance == null && numLives > 0)
        {
            respawnTimer -= Time.deltaTime;

            if (respawnTimer <= 0)
            {
                SpawnPlayer();
            }
        }

        nextEnemy -= Time.deltaTime;

        if (nextEnemy <= 0)
        {
            nextEnemy = rateOfSpawn;
            rateOfSpawn *= 0.9f;
            if (rateOfSpawn < 2)
            {
                rateOfSpawn = 2;
            }
            Vector3 offset = Random.onUnitSphere;
            offset.z = 0;
            offset = offset.normalized * spawnDistance;
            Instantiate(SlimeLargePrefab, transform.position + offset, Quaternion.identity);
        }
    }

    void SpawnPlayer()
    {
        numLives--;
        respawnTimer = 1;
        playerInstance = (GameObject)Instantiate(wizardPrefab, transform.position, Quaternion.identity);
    }

    void OnGUI()
    {
        if (numLives > 0 || playerInstance != null)
        {
            GUI.Label(new Rect(10, 0, 100, 50), "Lives Left: " + numLives);
        }
        else
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Game Over!");
        }
    }
}
