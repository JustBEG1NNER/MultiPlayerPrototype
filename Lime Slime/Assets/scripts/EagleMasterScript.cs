using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EagleMasterScript : MonoBehaviour
{

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentwaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);

      
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
           seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentwaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null)
         return;
        if (currentwaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentwaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.velocity = ((Vector2)path.vectorPath[currentwaypoint] - rb.position).normalized * speed;
        // new Vector2(direction.x , direction.y)*speed;
        //  rb.AddForce(force);

        float distance =Vector2.Distance(rb.position, path.vectorPath[currentwaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentwaypoint++;
        }
    }
}
