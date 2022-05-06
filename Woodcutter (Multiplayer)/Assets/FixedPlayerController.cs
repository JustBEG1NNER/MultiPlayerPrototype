using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixedPlayerController : MonoBehaviour
{

    public PhotonView photonView;
    public  Rigidbody2D rb;
    public Animator anim;

    public Text PlayerNameText;

    public LayerMask Floor; //add a layer named "Floor"
    public GameObject Bulletobject;
    public GameObject PlayerCamera;
    public GameObject ControlCanvas;

    public Transform FirePos;
    public Transform groundCheckCollider; //empty object tas kaag mo sa baba akng player postion
    public Transform Hero_Position;

    public float horizontalMove;
    public float speed;
    public float jumpHeight = 10;
    public float fallMultiplier = 3f;
    public float lowJumpMultiplier = 2f;

    public bool isGrounded = false;
    private bool facingRight = true;
    public bool moveleft;// Main var sa movement >> Pc control
    public bool moveright;// Main var sa movement >> Pc control
    public float verticalMove;

    public bool moveLeft_UI; // UI >> Selpon control
    public bool moveRight_UI;// UI >> selpon control
    public bool jumpButton_UI;// UI >> selpon control
    public bool fireball_UI;// UI >> selpon control

    public bool disableMove=false;

    public float cayoteTimeCounter;
    public float cayoteTime=1f;
        

    private void Awake()
    {
        // ChatInputField = GameObject.Find("ChatInputField").GetComponent<InputField>();

        if (photonView.isMine)
        {
            ControlCanvas.SetActive(true);
            PlayerCamera.SetActive(true);
            PlayerNameText.text = PhotonNetwork.playerName;
            //  PlayerNameText.text = "huyy";
        }
        else
        {
            PlayerNameText.text = photonView.owner.name;
            PlayerNameText.color = Color.blue;
        }

    }

    void Update()
    {
        if (photonView.isMine)
        {
        
            health health = gameObject.GetComponent<health>();
            
         //   Debug.Log(health.Hp);
            if (health.Hp > 0 && health.Hp <= 100)
            {
                //    disableMove = true;
                groundCheck(); // check kung nasa daga or mayo       

                //  if(!disableMove)
                checkInput(); //all input igdi hehe

                moveplayer(); // for left and right
                SpriteFlip(); //flipping sprite left and right
                JumpFallAnim();

            }       

        }    
    }

    void JumpFallAnim()
    {
        /*  */
        if (rb.velocity.y < -1f) { verticalMove = -1; } // < -2.1 ta yan ang at least na bilis kang pag bagsak mo bago iplay si falling animation
        else if (rb.velocity.y > 0.1f) { verticalMove = 1; } //  0.1 for margin of error
        else { verticalMove = 0; }

      

    }

    public void groundCheck()
    {
      

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, 0.3f, Floor);
        if (colliders.Length > 0)
        {
            isGrounded = true;
            //  Debug.Log("nasa daga");      
            
        } 
        else
        {
            isGrounded = false;
           
          
            //  Debug.Log("mayo sa daga");        
            //   Invoke("cayote_time", cayoteTime);
        }


        if (isGrounded)
        {
            cayoteTimeCounter = cayoteTime;
        }
        else
        {
            cayoteTimeCounter -= Time.deltaTime;
        }
                

        if(verticalMove > 0)
        {
            cayoteTimeCounter = 0;
        }


       




        if (rb.velocity.y < 0 && cayoteTimeCounter <= 0) 
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
              Debug.Log("falling");
        }
        else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space) || !jumpButton_UI)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
           //    Debug.Log("going Up");
        }

        if (verticalMove == -1 && cayoteTimeCounter < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    //
       
    }


  
//
    private void checkInput() // for pc
    {
   
        if ( moveLeft_UI)// Input.GetKey("a") ||
        {
            moveleft = true;
        }
        else
        {
            moveleft = false;
        }
        /////////////////////////////////////
        if ( moveRight_UI) //Input.GetKey("d") || 
        {
            moveright = true;
        }
        else
        {
            moveright = false;
        }
        if ( fireball_UI) // Input.GetKeyDown(KeyCode.Mouse1) ||
        {
            fireball();                      
        }
       
        fireball_UI = false;
        

        if ( jumpButton_UI) //Input.GetKeyDown(KeyCode.Space)|| 
        {
            if (cayoteTimeCounter>0)
            {
                rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            }
            
            Debug.Log("jump preesessed ");
            jumpButton_UI = false;

        }

   //    
    }
    
    public void fireball()
    {
        GameObject obj = PhotonNetwork.Instantiate(Bulletobject.name, new Vector2(FirePos.transform.position.x, FirePos.transform.position.y), Hero_Position.transform.rotation,0);
    }

    /////////////////////////////////////////////////////////////// UI controls para sa cp
    public void fireballPointerDown()
    {       if (photonView.isMine)     {      fireball_UI = true;       }       }
    public void jumpPointerDown()
    {       if (photonView.isMine)     {      jumpButton_UI = true;     }       }

    ///////////////////////////////////////////////////////////////
    public void PointerDownLeft()
    {       if (photonView.isMine)     {       moveLeft_UI = true;      }       }
    public void PointerUpleft()
    {       if (photonView.isMine)     {       moveLeft_UI = false;     }       }
    public void PointerDownRight() 
    {      if (photonView.isMine)      {       moveRight_UI = true;     }       }
    public void PointerUpRight()
    {      if (photonView.isMine)      {       moveRight_UI = false;    }       }
    ////////////////////////////////////////////////////////////////


    void SpriteFlip() {
        
        if (horizontalMove < 0 && facingRight)//if you press the left arrow and your facing right then you will face left;
        {
            photonView.RPC("Flip", PhotonTargets.AllBuffered);
        }
        else if (horizontalMove > 0 && !facingRight)//f you press the right arrow and you are facing right then you will face
        {
            photonView.RPC("Flip", PhotonTargets.AllBuffered);
        }
    }

    [PunRPC]
    private void Flip()
    {
        facingRight = !facingRight;//if facing right is true it will be false and if it is false it wili be true
        Hero_Position.transform.Rotate(0f, 180f, 0f); //skin sprite 
        
    }

    private void moveplayer()
    {
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);

        //if i press the left button
        if (moveleft)
            horizontalMove = -speed;
        //if i press the right button
        else if (moveright)
            horizontalMove = speed;
        //if i am not pressing any button
        else
            horizontalMove = 0;

       
        if (horizontalMove > 0 || horizontalMove < 0)
        {
            anim.SetBool("isRunning", true);
            //  Debug.Log("walking");
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (horizontalMove == 0)
        {
            //   Debug.Log("idle");
        }

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheckCollider.position, 0.3f);
        //  Gizmos.DrawWireSphere(playerHead.position, 0.3f);

    }
}
