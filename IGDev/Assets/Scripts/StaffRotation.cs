using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffRotation : MonoBehaviour
{
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeRotation();
        FlipSprite();
    }

    void ChangeRotation()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        //Debug.Log(mousePos.x - transform.position.x + "mouseX");
        //Debug.Log(mousePos.y - transform.position.y + "mouseY");

        transform.up = direction;
    }

    void FlipSprite()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if ((mousePos.x - transform.position.x) < 0.0f)
        {
            sprite.flipX = true;
            //transform.position = new Vector3(transform.position.x - 0.35f, transform.position.y, transform.position.z);
        }
        if ((mousePos.x - transform.position.x) > 0.0f)
        {
            sprite.flipX = false;
        }
    }
}
