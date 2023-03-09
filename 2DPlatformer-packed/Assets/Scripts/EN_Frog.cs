using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EN_Frog : MonoBehaviour
{
    public bool facingRight;
    private bool jump;

    public float maxSpeed = 5.0f;
    public float maxForce = 365.0f;
    public float horizonInput = 1.0f;

    protected SpriteRenderer spriteRenderer;
    protected Animator animationController;
    protected Rigidbody2D rb;

    protected BoxCollider2D floorTrigger;
    protected CapsuleCollider2D characterCollider;
    
    public float speed = 5.0f;
    void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        floorTrigger = GetComponent<BoxCollider2D>();

        characterCollider = GetComponent<CapsuleCollider2D>() ;

        if(facingRight == false)
        {
            Flip();
        }
    }
    void FixedUpdate()
    {
        if(Mathf.Abs(horizonInput * (rb.velocity.x)) < maxSpeed)
        {
            rb.AddForce(horizonInput * Vector2.right * maxForce);

        }
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    void OneTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Level")
        {
            if(other.attachedRigidbody.velocity.y < -0.1f)
            {
                horizonInput = 0;
                Destroy(other.gameObject);
            }
        }
    }
}
