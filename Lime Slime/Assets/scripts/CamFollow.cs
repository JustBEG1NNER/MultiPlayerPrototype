using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    //private Vector3 offset;

    public Vector3 cameraOffset = new Vector3(0, 0, -10);
    [Range(0, 20)]

    public float followSpeed = 2.4f;

    /// /////////////


    [Range(1, 10)]
    public float zoomsize = 3;
    public Camera MainCam;


    void Update()
    {
        MainCam.orthographicSize = zoomsize;
    }

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + cameraOffset;
        // Vector3 smoothedPosition = Vector3.Lerp(transform.position,targetPosition,smoothness* Time.fixedDeltaTime);

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.fixedDeltaTime);

        // transform.position = targetPosition; 
    }



    public void ZoomIn_ButtonPressed()
    {
        if (zoomsize <= 0) { zoomsize = 0.5f; }
        else { zoomsize -= 0.5f; }
    }

    public void ZoomOut_ButtonPressed()
    {
        if (zoomsize >= 10) { zoomsize = 10; }
        else { zoomsize += 0.5f; }
    }
}
