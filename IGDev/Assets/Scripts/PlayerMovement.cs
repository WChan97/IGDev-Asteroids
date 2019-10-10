using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer sprite;
    float speed = 4.0f;
    float wizardBoundary = 0.3f;
    // Start is called before the first frame update
    void Start()
    {

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


        transform.position = pos;

        //Check and flip sprite if mouse is on either side of wizard.
        FlipSprite();
    }

    void FlipSprite()
    {
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
}
