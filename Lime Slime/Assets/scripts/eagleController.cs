using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagleController : MonoBehaviour
{
   
    [SerializeField] private Transform target;
    
    void Update()
    {
       
        if (transform.position.x > target.transform.position.x)
        {
            // transform.Rotate(0f, 0f, 0f);//look right
          
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
          //  transform.Rotate(0f, 180f, 0f);//left
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
       
    }
}

