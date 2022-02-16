using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fireball : Photon.MonoBehaviour
{
    public bool MoveDir = false; //false (right), true (left)
    public float MoveSpeed;
    public float DestroyTime;
    public int fireball_damage = 20;
    private void Awake()
    {
        StartCoroutine("DestroyByTime");
    }

    IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(DestroyTime);
        this.GetComponent<PhotonView>().RPC("DestroyObject", PhotonTargets.AllBuffered);
    }


   


    private void Update()
    {

        if (!MoveDir)
            transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
        else
            transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.isMine)
            return;

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null && (!target.isMine  || target.isSceneView))
        {
            if (target.tag== "Player"){
                target.RPC("ReduceHealth", PhotonTargets.AllBuffered, fireball_damage);
                Debug.Log("player hit");
            }
  
            this.GetComponent<PhotonView>().RPC("DestroyObject", PhotonTargets.AllBuffered);
        }
    }

    [PunRPC]
    public void ChangeDir_left()
    {
        MoveDir = true;
    }

    [PunRPC]
    public void DestroyObject()
    {
            Destroy(this.gameObject);      
    }
}
         
