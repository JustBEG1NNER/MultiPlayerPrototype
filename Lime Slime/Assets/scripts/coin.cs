using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) // kapagnag duta sa collider na may istrigger
    {
        coinSystem coinSystem = collision.GetComponent<coinSystem>();



        if (collision.gameObject.tag == "Player")
        {          
            coinSystem.addCoin(1);
            Destroy(gameObject);
        }

      
    }
}
