using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyText : MonoBehaviour
{
    public float DestroyTime = 4f;
    private void OnEnable()
    {
        Destroy(gameObject, DestroyTime);
    }
   
}
