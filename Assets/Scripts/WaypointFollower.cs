using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;

    private int CurrentWaypointIndex;

    [SerializeField] private float speed = 2f;

    private void Update()
    {
     if(Vector2.Distance(waypoints[CurrentWaypointIndex].transform.position, transform.position) < .1f)
        {
            CurrentWaypointIndex++;
            if(CurrentWaypointIndex >= waypoints.Length)
            {
                CurrentWaypointIndex = 0;
            }
            flip();
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[CurrentWaypointIndex].transform.position, Time.deltaTime * speed);
    }

    private void flip()
    {
        Vector3 flip = transform.localScale;
        flip.x *= -1;
        transform.localScale = flip;
    }
}
