using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : Photon.MonoBehaviour
{
    public Slider HPslider;
    public int Hp;
    public Animator anim;

    public Rigidbody2D playerRB;
    public CapsuleCollider2D playerCol;
   // public SpriteRenderer playerSprite;
  
        

    private void Start()
    {     
        HPslider.minValue = 0;
        HPslider.maxValue = 100;      
        Hp = 100;

    }
   
    [PunRPC]
    public void ReduceHealth(int damage)
    {    
        ModifyHealth(damage);
    }


    private void Update()
    {
        if (photonView.isMine) {    HPslider.value = Hp;      }
        else                   {    HPslider.value = Hp;      }
           
    }

    private void ModifyHealth(int damage)
    {
       
        if (photonView.isMine)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                Debug.Log("die");
           //     playerRB.gravityScale = 0;                   
                photonView.RPC("dead", PhotonTargets.AllBuffered);

            }
            else
            {              
                Debug.Log("hurt me");
                anim.SetTrigger("hurt");

            }              
        }
        else
        {
            Hp -= damage;
            Debug.Log("hurt other");
            //  anim.SetTrigger("hurt");
        }
    }
     
[PunRPC]
    void dead()
    {
        anim.SetBool("isDead", true);
        anim.SetBool("isRunning", false);

        Invoke("Respawn", 5);
    }

    
    void Respawn()
    {   
       Hp = 100;     
     //  playerRB.gravityScale = 1;
       anim.SetBool("isDead", false);

    }



}