using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrawlerController : MonoBehaviour
{
    public float moveSpeed;
    public GameObject[] wayPoints;

    int nextWaypoint = 1;
    float distToPoint;

    // Update is called once per frame
    void Update()
    {
        distToPoint = Vector2.Distance(transform.position, wayPoints[nextWaypoint].transform.position);
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextWaypoint].transform.position, moveSpeed * Time.deltaTime) ;

        if(distToPoint < 0.1f)
        {
            TakeTurn();
        }
    }

    void TakeTurn()
    {
        Vector3 currRot = transform.eulerAngles;
        currRot.z += wayPoints[nextWaypoint].transform.eulerAngles.z;
        transform.eulerAngles = currRot;
        ChooseNextWatPoint();
    }

    void ChooseNextWatPoint()
    {
        nextWaypoint++;
        if(nextWaypoint == wayPoints.Length)
        {
            nextWaypoint = 0;
        }
    }
}
