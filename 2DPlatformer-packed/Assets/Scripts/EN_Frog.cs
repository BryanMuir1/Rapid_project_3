using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EN_Frog : MonoBehaviour
{
    public float maxSpeed = 5.0f;
    public float horizonInput = 1.0f;

    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;

    protected BoxCollider2D floorTrigger;
    protected CapsuleCollider2D characterCollider;

    bool mustTurn;
    public Transform floorCheck;
    public LayerMask groundLayer;
    public float knockbackForce = 5;

    void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        floorTrigger = GetComponent<BoxCollider2D>();

        characterCollider = GetComponent<CapsuleCollider2D>();

    }
    private void Update()
    {
        rb.velocity = new Vector2(maxSpeed, rb.velocity.y);

        if (mustTurn == true)
        {
            Flip();
        }

    }
    void FixedUpdate()
    {
        mustTurn = !Physics2D.OverlapCircle(floorCheck.position, 0.5f, groundLayer);
    }
    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        maxSpeed *= -1;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.rigidbody.velocity.y < -0.1f)
            {
                horizonInput = 0;
                //characterCollider.enabled = false;
                Destroy(gameObject, 0.5f);
            }
            //            else
            //            {
            //                other.gameObject.GetComponent<HealthComponent>().TakeDamage(20);
            //                Vector2 difference = (transform.position - other.transform.position).normalized;
            //                Vector2 force = difference * knockbackForce;
            //                rb.AddForce(force, ForceMode2D.Impulse); //if you don't want to take into consideration enemy's mass then use ForceMode.VelocityChange

            //                characterCollider.enabled = false;
            //                StartCoroutine(EnableCollider());
            //                Destroy(other.gameObject);
            //            }
            //        }
            //    }

            //    IEnumerator EnableCollider()
            //    {
            //        yield return new WaitForSeconds(2);
            //        characterCollider.enabled = true;
        }
    }
}
