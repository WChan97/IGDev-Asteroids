using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if (gameObject.tag == "LargeSlime")
        {
            speed = 3.0f;
        }
        flingDelay = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

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
        transform.position = pos;


        if (player == null)
        {
            GameObject wizard = GameObject.FindWithTag("Player");
            if (wizard != null)
            {
                player = wizard.transform;
            }
        }

        if (player == null)
        {
            //If player isn't found again, retry next frame.
            return;
        }

        if (bounceRecently == false)
        {
            //Turn towards player
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            float zAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion desiredDir = Quaternion.Euler(0, 0, zAngle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredDir, rotationSpeed * Time.deltaTime);
        }
        bounceDelay -= Time.deltaTime;

        if (bounceDelay <= 0.0f) {
            bounceRecently = false;
        }

        //And move towards player
        Vector3 pos2 = transform.position;
        Vector3 velocity = new Vector3(0, speed * Time.deltaTime, 0);
        pos2 += transform.rotation * velocity;
        transform.position = pos2;

        

        flingDelay -= Time.deltaTime;
        if (flingDelay <= 0.0f)
        {
            Fling();
        }
    }

    void Bounce()
    {
        //Debug.Log("Bounce");
        Vector2 direction = new Vector2(transform.position.x - Random.Range(-180f, 180f), transform.position.y - Random.Range(-180f, 180f));
        transform.up = direction;
        bounceRecently = true;
        bounceDelay = 0.5f;
    }

    void Fling()
    {
        if (currentScene.name == "SpecialScene")
        {
            bounceRecently = true;
            bounceDelay = 0.2f;
            if (speed >= 2.0f && speed < 10.0f)
            { //All Slimes
                speed = 0.0f;
                flingDelay = 1.0f;
            }
            else if (speed == 0.0f)
            {//All fling ready slimes
                speed = 20.0f;
                flingDelay = 0.15f;

            }
            else
            { //Reset to normal speed.
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
        if (gameObject.layer == collide.gameObject.layer)
        {
            Bounce();
        }
    }

    
    void OnTriggerStay2D(Collider2D collide)
    {
        if (currentScene.name == "NormalScene")
        {
            if (gameObject.layer == collide.gameObject.layer)
            {
                Bounce();
            }
        }
    }
    
}
