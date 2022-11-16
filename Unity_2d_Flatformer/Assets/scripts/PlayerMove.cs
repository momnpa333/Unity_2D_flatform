using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    private void Update()
    {
        if(Input.GetButtonUp("Horizontal"))
        {
            //stop speed
            rigid.velocity = new Vector2(rigid.velocity.normalized.x*(0.5f), rigid.velocity.y);
        }
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        if(rigid.velocity.x<=(0.3f) && rigid.velocity.x >= (-0.3f))
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
    }
    void FixedUpdate()
    {
        //Move By key Control
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)//right
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed*(-1))//left
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
    }
}
