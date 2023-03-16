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

    bool mustTurn;
    public Transform floorCheck;
    public LayerMask groundLayer;
    bool Patrol; 

    void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        floorTrigger = GetComponent<BoxCollider2D>();

        characterCollider = GetComponent<CapsuleCollider2D>() ;
        Patrol = true; 
        //if(facingRight == false)
        //{
        //    Flip();
        //}

        
    }
    private void Update()
    {
        Debug.Log(mustTurn);
        //if (Patrol)
        //{
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        //}

        if (mustTurn == true)
        {
            Flip(); 
        }
       
    }
    void FixedUpdate()
    {
        mustTurn = !Physics2D.OverlapCircle(floorCheck.position, 0.5f, groundLayer);

        //if(Mathf.Abs(horizonInput * (rb.velocity.x)) < maxSpeed)
        //{
        //    rb.AddForce(horizonInput * Vector2.right * maxForce);

        //}
        //if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        //{
        //    rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        //}
    }
    void Flip()
    {
        Patrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        maxSpeed *= -1;  
        Patrol = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //if (other.gameObject.layer == LayerMask.NameToLayer("Level"))
        //{
        //    Flip();        
        //}
    }
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            if (other.rigidbody.velocity.y < -0.1f)
            {
                horizonInput = 0;
                Destroy(gameObject, 0.5f);
            }
            else
            {
                //Destroy(other.gameObject);
            }
        }
    }
}
