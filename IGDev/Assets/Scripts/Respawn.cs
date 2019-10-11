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
        //Since the scenes start empty, spawn a player in.
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInstance == null && numLives > 0)
        {
            respawnTimer = respawnTimer - Time.deltaTime;

            if (respawnTimer <= 0)
            {
                SpawnPlayer();
            }
        }

        nextEnemy = nextEnemy - Time.deltaTime;

        if (nextEnemy <= 0.0f)
        {
            nextEnemy = rateOfSpawn;
            rateOfSpawn = rateOfSpawn * 0.9f; // Increase the rate of spawn by getting 90% of its current value every spawn, until it reaches every 2 seconds. I'm not that evil.
            if (rateOfSpawn < 2.0f)
            {
                rateOfSpawn = 2.0f;
            }

            Vector3 offset = Random.onUnitSphere;
            offset.z = 0;
            offset = offset.normalized * spawnDistance;
            Instantiate(SlimeLargePrefab, transform.position + offset, Quaternion.identity); //Spawn Large Slime in a random distance away.
        }
    }

    void SpawnPlayer()
    {
        numLives--;
        respawnTimer = 1.0f;
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
