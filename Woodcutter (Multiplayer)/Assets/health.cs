using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : Photon.MonoBehaviour
{
    public Slider HPslider;
    public int Hp = 100;
    public Animator anim;

    private void Start()
    {
      
        HPslider.minValue = 0;
        HPslider.maxValue = 100;
        HPslider.value = 100;
    }
   
    [PunRPC]
    public void ReduceHealth(int damage)
    {
        ModifyHealth(damage);
    }

    private void ModifyHealth(int damage)
    {
        if (photonView.isMine)
        {
            HPslider.value -= damage;
            Debug.Log("hurt me");
            anim.SetTrigger("hurt");

            if (HPslider.value <= 0)
            {
                Debug.Log("die");
            }
        }
        else
        {
            HPslider.value -= damage;
            Debug.Log("hurt other");
        }
        

    }
}