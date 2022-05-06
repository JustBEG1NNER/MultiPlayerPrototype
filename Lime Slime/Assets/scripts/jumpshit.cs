using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpshit : MonoBehaviour
{
    private Animator animator;
   
    float HorizontalMove;

    public float speed = 8f;
    public float jumpingPower = 15f;
   

    private bool isJumping;

    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    /// ////////////////////////

    
    bool stopcounter;
    bool facingRight = true;

    public Transform Slime_Position;

    /// ///////////////////////////
    public float fallMultiplier = 6f;
    public float lowJumpMultiplier = 4f;

    /// /////////////////////////
  
    private bool moveleft;
    private bool moveright;
    private float X_Input;

    public float jumpcd = 0;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
       
       //  Debug.Log(rb.velocity.x + "," + rb.velocity.y + "        " + verticalMove+ " , "+HorizontalMove);
     //   Debug.Log(IsGrounded());


        JumpRising_FallingGravity();
        cayoteTime_jumpBuffer();
       // keyboardControls();
        moveplayer();
        Flip();


        JumpFallAnim();
        walkAnim();
    }

    private void FixedUpdate()
    {
        //code for moving left and right
      
       
            rb.velocity = new Vector2(X_Input * speed, rb.velocity.y);
       
          
        
    }
    /*
    void keyboardControls()
    {
        //////////////////////////////////////
        if (Input.GetKey("a"))
        {
            moveleft = true;
        }
        else
        {
            moveleft = false;
        }
        /////////////////////////////////////
        if (Input.GetKey("d"))
        {
            moveright = true;
        }
        else
        {
            moveright = false;
        }
        /////////////////////////////////////
    }
    */


    void JumpFallAnim()
    {
        /* 

        if (rb.velocity.y < -1f) { verticalMove = -1; } // < -2.1 ta yan ang at least na bilis kang pag bagsak mo bago iplay si falling animation
        else if (rb.velocity.y > 0.1f) { verticalMove = 1; } //  0.1 for margin of error
        else { verticalMove = 0; }

        animator.SetFloat("Move", verticalMove);
         */

    }

    void walkAnim()
    {

        

     if (rb.velocity.x==0  && IsGrounded()) { HorizontalMove = 0; } //idle
     else if ((rb.velocity.x < -0.1f || rb.velocity.x > 0.1f ) && IsGrounded() ) { HorizontalMove = 1; } // < -2.1 ta yan ang at least na bilis kang pag bagsak mo bago iplay si falling animation
     else if ( rb.velocity.y > 0.1 && !IsGrounded()) { HorizontalMove = 2; }
     else if ( rb.velocity.y < 0 && !IsGrounded()) { HorizontalMove = 3; }


        animator.SetFloat("Move",HorizontalMove);
    }

    void moveplayer()
    {
        //if i press the left button
        if (moveleft)
        { X_Input = -1; }
        //if i press the right button
        else if (moveright)
        { X_Input = 1; }
        //if i am not pressing any button
        else X_Input = 0;
    }


    void Flip()
    {
        if (X_Input < 0 && facingRight)//if you press the left arrow and your facing right then you will face left;
        {
            facingRight = !facingRight;//if facing right is true it will be false and if it is false it wili be true
            Slime_Position.transform.Rotate(0f, 180f, 0f);
        }
        else if (X_Input > 0 && !facingRight)
        {
            facingRight = !facingRight;
            Slime_Position.transform.Rotate(0f, 180f, 0f);
        }
    }



    void JumpRising_FallingGravity()
    {
        
        if (rb.velocity.y < 0.1f)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
          //  Debug.Log("falling");
        }
        else if (rb.velocity.y > 0 )
        {

            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
          //  Debug.Log("rising");
        }
    }

    void cayoteTime_jumpBuffer()
    {
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

     

        if (jumpBufferCounter == 0)
        {

        }
        else if (stopcounter){
            jumpBufferCounter -= Time.deltaTime;
          
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }
       
    }
    public void jumppressed()
    {
        Debug.Log("jumppressesss"); // just set it on a button ui
       
        jumpBufferCounter = jumpBufferTime;
        stopcounter = true;

        if ( rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }
    }

    private IEnumerator JumpCooldown()
    {
       
        isJumping = true;
        yield return new WaitForSeconds(jumpcd);
        isJumping = false;
    }



    

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }


    ///////////////////////////////////////////////////////////////
    public void PointerDownLeft() { moveleft = true; }
    public void PointerUpleft() { moveleft = false; }
    public void PointerDownRight() { moveright = true; }
    public void PointerUpRight() { moveright = false; }
    ////////////////////////////////////////////////////////////////
     void OnDrawGizmosSelected()
    {
          Gizmos.color = Color.white;
           Gizmos.DrawWireSphere(groundCheck.transform.position, 0.3f);
        
    }
}

/* 

if (rb.velocity.y < -1f) { verticalMove = -1; } // < -2.1 ta yan ang at least na bilis kang pag bagsak mo bago iplay si falling animation
else if (rb.velocity.y > 0.1f) { verticalMove = 1; } //  0.1 for margin of error
else { verticalMove = 0; }

animator.SetFloat("Move", verticalMove);

    }

    void walkAnim()
{

    if (rb.velocity.x < -0.1f) { HorizontalMove = 1; } // < -2.1 ta yan ang at least na bilis kang pag bagsak mo bago iplay si falling animation
    else if (rb.velocity.x > 0.1f) { HorizontalMove = 1; } //  0.1 for margin of error
    else { HorizontalMove = 0; }

    animator.SetFloat("Move", HorizontalMove);
} */