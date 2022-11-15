using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WaypointPatrol : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    //public bool addInitialPosAsWaypoint;

    private int currentWaypointIndex;
    private int currentWaypointListCount;
    // Start is called before the first frame update
    void Start()
    {
        //if (addInitialPosAsWaypoint) waypoints.Add(transform);
        currentWaypointListCount = waypoints.Length;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % currentWaypointListCount; //Resto
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
}
