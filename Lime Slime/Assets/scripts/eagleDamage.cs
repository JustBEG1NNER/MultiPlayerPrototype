using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class eagleDamage : MonoBehaviour
{
    
    public int damage = 15;


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerHp player = hitInfo.GetComponent<PlayerHp>();// "PlayerHp is a fucking class name"
        if (player != null)
        {
            player.TakeDamage(damage);


        }
        //   Destroy(gameObject);

        Debug.Log(hitInfo.name);
    }

    
    private void OnTriggerStay2D(Collider2D hitInfo)
    {


       

       
    }
}
