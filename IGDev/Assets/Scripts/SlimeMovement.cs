﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeMovement : MonoBehaviour
{
    Scene currentScene;
    public Transform player;
    float speed = 5.0f;
    float rotationSpeed = 180f;
    bool bounceRecently = false;
    float bounceDelay;
    float flingDelay;
    float wizardBoundary = -2f;
    public GameObject flingPrefab;
    SpriteRenderer flingSpriteRend;
    // Start is called before the first frame update
    void Start()
    {
        flingSpriteRend = gameObject.GetComponent<SpriteRenderer>();
        currentScene = SceneManager.GetActiveScene();

        if (gameObject.tag == "LargeSlime")
        {
            // Large Slimes are slower
            speed = 3.0f;
        }

        flingDelay = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        //Define Boundaries (Same in PlayerMovement)
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthCam = Camera.main.orthographicSize * screenRatio;


        //Keep slime in boundary. Vertical First, then Horizontal. (Same as PlayerMovement, you can tell since it's wizardBoundary hahaha.)
        if (pos.y + wizardBoundary > Camera.main.orthographicSize)
        {
            pos.y = Camera.main.orthographicSize - wizardBoundary;
        }
        if (pos.y - wizardBoundary < -Camera.main.orthographicSize)
        {
            pos.y = -Camera.main.orthographicSize + wizardBoundary;
        }
        if (pos.x + wizardBoundary > widthCam)
        {
            pos.x = widthCam - wizardBoundary;
        }
        if (pos.x - wizardBoundary < -widthCam)
        {
            pos.x = -widthCam + wizardBoundary;
        }
        transform.position = pos;

        //Find Player (because of the player respawn mechanic, slimes will need to search for one again)
        if (player == null)
        {
            GameObject wizard = GameObject.FindWithTag("Player");
            if (wizard != null)
            {
                player = wizard.transform;
            }
        }

        //And move towards player
        Vector3 pos2 = transform.position;
        Vector3 velocity = new Vector3(0, speed * Time.deltaTime, 0);
        pos2 += transform.rotation * velocity;
        transform.position = pos2;

        if (player == null)
        {
            //If player isn't found again, retry next frame.
            return;
        }

        
        if (bounceRecently == false)
        {
            //Turn towards player, which in turn will move slime towards them as above.
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            float zAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion desiredDir = Quaternion.Euler(0, 0, zAngle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredDir, rotationSpeed * Time.deltaTime);
        }

        bounceDelay -= Time.deltaTime;
        //Count down for bounce() to work again
        if (bounceDelay <= 0.0f) {
            bounceRecently = false;
        }

        flingDelay -= Time.deltaTime;
        //Countdown for fling to activate.
        if (flingDelay <= 0.0f)
        {
            Fling();
        }
        if (speed == 0.0f)
        {
            if (flingSpriteRend.color == Color.red)
            {
                flingSpriteRend.color = Color.white;
            }
            else
            {
                flingSpriteRend.color = Color.red;
            }
        }
    }

    void Bounce()
    {
        //Bounce Mechanics, I didn't know how to use physics, so I theorised my own way instead.
        //Debug.Log("Bounce");
        //Essentially, when a slime collides with another slime, change their direction wildly.
        Vector2 direction = new Vector2(transform.position.x - Random.Range(-360f, 360f), transform.position.y - Random.Range(-360f, 360f));
        transform.up = direction;
        bounceRecently = true;
        bounceDelay = 0.5f;
    }

    void Fling()
    {
        if (currentScene.name == "SpecialScene")
        { 
            //Special innovation where I change the speeds of the slimes to give an illusion of flinging.
            //Sprite Flasing Red to Magenta is also included.

            if (speed >= 2.0f && speed < 10.0f)
            {//All Normal Slimes
                speed = 0.0f;
                flingDelay = 1.0f;
                Instantiate(flingPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.Euler(0, 0, 0));
            }
            else if (speed == 0.0f)
            {//All fling ready slimes
                bounceRecently = true;
                bounceDelay = 0.15f;
                if (flingSpriteRend != null)
                {
                    flingSpriteRend.color = Color.magenta;
                }
                speed = 20.0f;
                flingDelay = 0.15f;

            }
            else
            { //Reset to normal.
                flingSpriteRend.color = Color.white;
                speed = 3.0f;
                if (gameObject.tag == "LargeSlime")
                {
                    speed = 2.0f;
                }
                flingDelay = 5.0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        //Bounce when slimes collide.
        if (gameObject.layer == collide.gameObject.layer)
        {
            if (bounceRecently == false)
            {
                Bounce();
            }
            
        }
    }

    
    void OnTriggerStay2D(Collider2D collide)
    {
        if (gameObject.layer == collide.gameObject.layer)
        {
            if (bounceRecently == false)
            {
                Bounce();
            }
        }
    }
    
}
