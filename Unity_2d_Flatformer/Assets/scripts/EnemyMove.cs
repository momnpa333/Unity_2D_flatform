using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    //물리엔진
    Rigidbody2D rigid;
    public int nextMove;
    Animator anim;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
         //주어진 시간이 지난뒤,지정된 함수를 실행하는 함수
        Invoke("Think", 3);
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);


        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x+nextMove,rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec*0.1f, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    //nextmove random
    void Think()
    {
        //-1<=x<2 -1,0,1 int
        nextMove = Random.Range(-1, 2);
        float nextThinkTime = Random.Range(1f, 4f);
        Invoke("Think", nextThinkTime);

        anim.SetInteger("WalkSpeed",nextMove);
        if(nextMove!=0)
            spriteRenderer.flipX = nextMove == 1;
    }

    void Turn()
    {
        nextMove = nextMove * -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 2);
    }
}
