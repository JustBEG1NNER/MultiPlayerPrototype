using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PhotonView photonView;
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject PlayerCamera;
    public SpriteRenderer sr;
    public Text PlayerNameText;
    public bool IsGrounded= false;
    public float MoveSpeed;
    public float JumpForce;


    public bool isGrounded = false;
    public Transform groundCheckCollider; //empty object tas kaag mo sa baba akng player postion
    public LayerMask Floor; //add a layer named "Floor"
  //  public Transform playerHead; //empty object tas kaag mo sa baba akng player postion

    public float jumpHeight=10;
    public float fallMultiplier = 3f;
    public float lowJumpMultiplier = 2f;

  ///  public InputField ChatInputField;
    bool freezeMovement;

    private void Awake()
    {
       // ChatInputField = GameObject.Find("ChatInputField").GetComponent<InputField>();

        if (photonView.isMine)
        {
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
  
    private void Update()
    {
        if (photonView.isMine)
        {
            CheckInput();

            if (!freezeMovement)
                {
                    
                }
                else
                {

                }
            
           

           

           
            groundCheck();
            /* 
                        if (ChatInputField.text.Length > 0)
                        {
                            freezeMovement = true;
                        }
                        if (ChatInputField.text.Length == 0)
                        {
                            freezeMovement = false;
                        }
            */
        }


    }



    public void groundCheck()
    {
        isGrounded = false;
       
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, 0.3f, Floor);
        if (colliders.Length > 0)
        {
            isGrounded = true;
          //  Debug.Log("nasa daga");
        }
        else
        {
          //  Debug.Log("mayo sa daga");
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
          //  Debug.Log("grav");
        }
        else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {

            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
         //   Debug.Log("jumpo");
        }

    }








    private void CheckInput()
    {
        var move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        transform.position += move * MoveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.A))
            photonView.RPC("FlipTrue", PhotonTargets.AllBuffered);

        if (Input.GetKeyDown(KeyCode.D))
            photonView.RPC("FlipFalse", PhotonTargets.AllBuffered);


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);


        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump preesessed ");
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }

    }

 [PunRPC]
  private void FlipTrue()
    {
        sr.flipX = true;
    }
     
 [PunRPC]
private void FlipFalse()
    {
        sr.flipX = false;
    }

    void OnDrawGizmosSelected()
    {
          Gizmos.color = Color.white;
           Gizmos.DrawWireSphere(groundCheckCollider.position, 0.3f); 
      //  Gizmos.DrawWireSphere(playerHead.position, 0.3f);

    }

}
