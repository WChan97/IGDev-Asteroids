﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public int health = 3;
    float invulnTime = 0.0f;
    public bool isSlime;
    int currentLayer;
    public GameObject SlimeSmallPrefab;
    public GameObject SlimeLargePrefab;
    public AudioClip hit;
    SpriteRenderer spriteRend;
    private ScoreHandler scoreHandler;

    // Start is called before the first frame update
    void Start()
    {
        GameObject respawnScore = GameObject.FindWithTag("Respawn");
        scoreHandler = respawnScore.GetComponent<ScoreHandler>();
        currentLayer = gameObject.layer;
        spriteRend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            DestroyTarget();
        }

        if (invulnTime > 0)
        {
            invulnTime -= Time.deltaTime;

            //If player's invuln time is up, return to object's previous layer
            if (invulnTime <= 0 && isSlime == false)
            {
                gameObject.layer = currentLayer;
                if (spriteRend != null)
                {
                    spriteRend.enabled = true;
                }
            }
            else {
                if (spriteRend != null)
                {
                    spriteRend.enabled = !spriteRend.enabled;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        if (gameObject.layer != collide.gameObject.layer) //Check if slimes are not colliding together
        {
            health = health - 1;
            //Debug.Log(gameObject.name + "'s health is: " + health);
            //When hit, send to invuln layer.
            if (isSlime == false)
            {
                invulnTime = 2.0f;
                gameObject.layer = 11;
            }
            else if (isSlime == true) // Split slime
            {
                AudioSource.PlayClipAtPoint(hit, Camera.main.transform.position);
                Split();
            }
        }
    }

    void Split()
    {
        if (gameObject.tag == "LargeSlime")
        {
            //Debug.Log("Split");
            Instantiate(SlimeSmallPrefab, new Vector3(transform.position.x - Random.Range(-0.8f, 0.8f), transform.position.y - Random.Range(-0.8f, 0.8f), 0), Quaternion.Euler(0, 0, 90));
            Instantiate(SlimeSmallPrefab, new Vector3(transform.position.x - Random.Range(-0.8f, 0.8f), transform.position.y - Random.Range(-0.8f, 0.8f), 0), Quaternion.Euler(0, 0, 90));
            Instantiate(SlimeSmallPrefab, new Vector3(transform.position.x - Random.Range(-0.8f, 0.8f), transform.position.y - Random.Range(-0.8f, 0.8f), 0), Quaternion.Euler(0, 0, 90));
            Instantiate(SlimeSmallPrefab, new Vector3(transform.position.x - Random.Range(-0.8f, 0.8f), transform.position.y - Random.Range(-0.8f, 0.8f), 0), Quaternion.Euler(0, 0, 90));
            scoreHandler.IncrementScore(40);
        }
        else {
            scoreHandler.IncrementScore(15);
        }
    }

    void DestroyTarget() {
        Destroy(gameObject);
    }

    void OnGUI()
    {
        if (health > 0 && gameObject.tag == "Player")
        {
            GUI.Label(new Rect(10, 20, 100, 50), "Health: " + health);
        }
        if (health == 0 && gameObject.tag == "Player")
        {
            GUI.Label(new Rect(10, 20, 100, 50), "Health: ");
        }
    }
}
