using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public int nextMove; //기본 행동
    Animator anim;
    CapsuleCollider2D colr;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        colr = GetComponent<CapsuleCollider2D>();
        Think();    
    }

    void FixedUpdate()
    {
        // Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // Platform Check
        Vector2 frontVector = new Vector2(rigid.position.x+ nextMove*0.2f, rigid.position.y);
        Debug.DrawRay(frontVector, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(frontVector, Vector3.down, 1, LayerMask.GetMask("Platform"));

        if (rayHit.collider == null)
        {
            Turn();


        }


    }
    void Think() {

        //Set Next Active
        nextMove = Random.Range(-1,2);

        //Sprite Animation
        anim.SetInteger("WalkSpeed", nextMove);


        // Flip Sprite
        if (nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }

        //Recurisve
        float nextThinkTime = Random.Range(2f, 6f);
        Invoke("Think", nextThinkTime);
    }
    void Turn()
    {
        nextMove = nextMove * (-1);
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 5);
    }

    public void OnDamaged()
    {
        // Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // SpriteFlip Y
        spriteRenderer.flipY = true;

        // Collider Disable
        colr.enabled = false;

        // Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        // Destory
        Invoke("DeActive", 5f);
    }

    void DeActive() {
        gameObject.SetActive(false);
    }

}
