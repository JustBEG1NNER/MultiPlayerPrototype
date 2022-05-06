using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sampleBullet : Photon.MonoBehaviour
{
    private Rigidbody2D rb;
    public float Fireballspeed = 20f;
    public int Fireballdamage = 10;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * Fireballspeed;
        Invoke("DestroyFireball", 1);//this will happen after 2 seconds
    }

    void DestroyFireball()
    {
        this.GetComponent<PhotonView>().RPC("DestroyObject", PhotonTargets.AllBuffered);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.isMine)
            return;

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null && (!target.isMine || target.isSceneView))
        {
            if (target.tag == "Player")
            {
                target.RPC("ReduceHealth", PhotonTargets.AllBuffered, Fireballdamage);
                Debug.Log(" hit" + collision.name);
            }
           
        }

        this.GetComponent<PhotonView>().RPC("DestroyObject", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }


}
