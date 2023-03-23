using UnityEngine;
using System.Collections;
using UnityEngine.Events;

//--------------------------------------------
/*Better Character Controller Includes:
     - Fixed Update / Update Input seperation
     - Better grounding using a overlap box
     - Basic Multi Jump
 */
//--------------------------------------------
public class BetterCharacterController : MonoBehaviour
{
    protected bool facingRight = true;

    //Jump Values
    protected bool jumped;
    public int maxJumps;
    protected int currentjumpCount;
    public float jumpForce = 1000;
    public bool grounded;

    //Run value
    private float horizInput;
    public float speed = 5.0f;
    public float shiftSpeed = 10.0f;
    public float resetSpeed = 5.0f;

    //For walls
    public bool isWallSliding = false;
    public Transform wallCheck;
    public LayerMask wallLayer;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(8f, 16f);

    public Rigidbody2D rb;

    public LayerMask groundedLayers;

    protected Collider2D charCollision;
    protected Vector2 playerSize, boxSize;

    public Animator animator;

    public UnityEvent OnLandEvent;
    public ParticleSystem dust;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        charCollision = GetComponent<Collider2D>();
        playerSize = charCollision.bounds.extents;
        boxSize = new Vector2(playerSize.x, 0.05f);
    }

    void FixedUpdate()
    {
        
        //Box Overlap Ground Check
        Vector2 boxCenter = new Vector2(transform.position.x + charCollision.offset.x, transform.position.y + -(playerSize.y + boxSize.y - 0.01f) + charCollision.offset.y);
        grounded = Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundedLayers) != null;

        if(!isWallJumping)
        {
            //Move Character
            rb.velocity = new Vector2(horizInput * speed * Time.fixedDeltaTime, rb.velocity.y);
        }


        //Jump
        if (jumped == true)
        {
            

            rb.AddForce(new Vector2(0f, jumpForce));
            //Debug.Log("Jumping!");

            jumped = false;
        }
        if (!isWallJumping)
        {
        // Detect if character sprite needs flipping.
        if (horizInput > 0 && !facingRight)
        {
            FlipSprite();
        }
        else if (horizInput < 0 && facingRight)
        {
            FlipSprite();
        }
        }

    }

    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(horizInput));


        if (grounded)
        {
            currentjumpCount = maxJumps;
            Onlanding();
        }

        //Input for jumping ***Multi Jumping***
        if (Input.GetButtonDown("Jump") && currentjumpCount > 1)
        {
            jumped = true;

            currentjumpCount--;
           // Debug.Log("Should jump");

            animator.SetBool("isJumping", true);
        }

        //Get Player input 
        horizInput = Input.GetAxis("Horizontal");

        //Run
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = shiftSpeed;
            createdust();
           // Debug.Log("Should run");

            StartCoroutine (ResetSpeed());
        }

        wallSlide();
        wallJump();
    }

    public void Onlanding()
    {
        animator.SetBool("isJumping", false);
    }
    // Flip Character Sprite
    void FlipSprite()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    
    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer) ;
    }
    private void wallSlide()
    {
        if(IsWalled() && !grounded)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }
    private void wallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                facingRight = !facingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }
    private void StopWallJumping()
    {
        isWallJumping = false;
    }
    void createdust()
    {
        dust.Play();
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(2.0f);
        speed = resetSpeed;
        //Debug.Log("reset");
    }
}
