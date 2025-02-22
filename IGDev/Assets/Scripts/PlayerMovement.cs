﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Scene currentScene;
    private SpriteRenderer sprite;
    float speed = 4.0f;
    float hasteCooldown = 3.0f;
    float wizardBoundary = 0.3f;
    public AudioClip tele;
    public AudioClip haste;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene(); //Used for scene differences
    }

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        //GetAxis returns -1.0 - +1.0; Based on input.
        pos.y += Input.GetAxis("Vertical") * speed * Time.deltaTime;
        pos.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        //Define Boundaries
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthCam = Camera.main.orthographicSize * screenRatio;


        //Keep wizard in boundary. Vertical First, then Horizontal.
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

        if (currentScene.name == "SpecialScene")
        {
            //Special Scene's Iteration. This script handles movement and speed. So the two additions, teleport and increased movement speed are present.
            if (Input.GetKeyDown("q") == true || Input.GetKeyDown("space") == true)
            {
                AudioSource.PlayClipAtPoint(tele, Camera.main.transform.position);
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                pos = new Vector3(mousePos.x, mousePos.y, 0);
            }

            if (Input.GetKeyDown("e") == true || Input.GetKeyDown("left shift") == true && hasteCooldown <= 0.0f)
            {
                AudioSource.PlayClipAtPoint(haste, Camera.main.transform.position);
                speed = 9.0f;
                hasteCooldown = 3.0f;
            }
            hasteCooldown -= Time.deltaTime;
            if (hasteCooldown <= 1.5f) {
                speed = 4.0f;
            }
        }

        //Update positions
        transform.position = pos;

        //Check and flip sprite if mouse is on either side of wizard.
        FlipSprite();
    }

    void FlipSprite()
    {
        //If the mouse is on either side of the wizards sprite, flip so it's looking at the mouse.
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        if ((mousePos.x - transform.position.x) < 0.0f)
        {
            sprite.flipX = true;
        }
        if ((mousePos.x - transform.position.x) > 0.0f)
        {
            sprite.flipX = false;
        }
    }

    void OnGUI()
    {
        if (hasteCooldown <= 0.0f && currentScene.name == "SpecialScene")
        {
            GUI.Label(new Rect(10, 60, 100, 50), "Haste Ready");
            GUI.Label(new Rect(10, 70, 100, 50), "Press 'E'");
        }
    }
}
