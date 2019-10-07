using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer sprite;
    float speed = 5.0f;
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


        //Update Position
        transform.position = pos;
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
