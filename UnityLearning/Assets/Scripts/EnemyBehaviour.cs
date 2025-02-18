using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform Player;
    public Transform PatrolRoute;
    public List<Transform> Locations;
    private int _locationIndex = 0;
    private NavMeshAgent _agent; 
    void Update()
    {
        if(_agent.remainingDistance <0.2f && !_agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player").transform;
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }
    void InitializePatrolRoute()
    {
        foreach(Transform child in PatrolRoute)
        {
            Locations.Add(child);
        }
    }
    void MoveToNextPatrolLocation()
    {
        if (Locations.Count == 0)
        return;

        _agent.destination = Locations[_locationIndex].position;

        _locationIndex = ( _locationIndex + 1 ) % Locations.Count;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            _agent.destination = Player.position;
            Debug.Log("Enemy Detected!");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }
}
