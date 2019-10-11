using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject SlimeSmallPrefab;
    public GameObject SlimeLargePrefab;
    public AudioClip hit;
    public AudioClip splat;
    public int health = 3;
    float invulnTime = 0.0f;
    public bool isSlime;
    int currentLayer;
    SpriteRenderer spriteRend;
    private ScoreHandler scoreHandler;

    // Start is called before the first frame update
    void Start()
    {
        //Find score handler, and set the layer number to the object's layer index.
        //Also get their sprite renderer for flashing effects.
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
            //Self Explanitory, if health is below 1, the player/slime is dead.
            DestroyTarget();
        }

        if (invulnTime > 0)
        {
            //If player has invulnerable time, countdown, and flash sprite using SpriteRenderer.enabled for that oldschool effect.
            invulnTime -= Time.deltaTime;
            
            if (invulnTime <= 0 && isSlime == false)
            {
                //If player's invuln time is up, return to object's previous layer
                gameObject.layer = currentLayer;

                if (spriteRend != null)
                {
                    spriteRend.enabled = true;
                }
            }
            else { //Flash sprite when hit by alternating every frame.
                if (spriteRend != null)
                {
                    spriteRend.enabled = !spriteRend.enabled;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        if (gameObject.layer != collide.gameObject.layer && invulnTime <= 0) //Check if slimes are not colliding together, and if the player is not invulnerable.
        {
            health = health - 1;
            //Debug.Log(gameObject.name + "'s health is: " + health);
            if (isSlime == false)
            {
                //If collision occured, grant invuln time to player, and send them to a layer for safety.
                invulnTime = 2.0f;
                gameObject.layer = 11;
            }
            else if (isSlime == true) // Split slime
            {
                Split();
            }
        }
    }

    void Split()
    {
        if (gameObject.tag == "LargeSlime")
        {
            //Create smaller slimes, and increment the score.
            //Debug.Log("Split");
            AudioSource.PlayClipAtPoint(splat, Camera.main.transform.position);
            Instantiate(SlimeSmallPrefab, new Vector3(transform.position.x - Random.Range(-0.8f, 0.8f), transform.position.y - Random.Range(-0.8f, 0.8f), 0), Quaternion.Euler(0, 0, 90));
            Instantiate(SlimeSmallPrefab, new Vector3(transform.position.x - Random.Range(-0.8f, 0.8f), transform.position.y - Random.Range(-0.8f, 0.8f), 0), Quaternion.Euler(0, 0, 90));
            Instantiate(SlimeSmallPrefab, new Vector3(transform.position.x - Random.Range(-0.8f, 0.8f), transform.position.y - Random.Range(-0.8f, 0.8f), 0), Quaternion.Euler(0, 0, 90));
            Instantiate(SlimeSmallPrefab, new Vector3(transform.position.x - Random.Range(-0.8f, 0.8f), transform.position.y - Random.Range(-0.8f, 0.8f), 0), Quaternion.Euler(0, 0, 90));
            scoreHandler.IncrementScore(40);
        }
        else {
            AudioSource.PlayClipAtPoint(hit, Camera.main.transform.position);
            scoreHandler.IncrementScore(15);
        }
    }

    void DestroyTarget() {
        Destroy(gameObject);
    }

    void OnGUI()
    {
        //GUI.Label exists across multiple scripts. It's a little janky.
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
