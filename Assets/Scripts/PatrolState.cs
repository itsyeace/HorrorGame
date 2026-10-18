using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IEnemyState
{
    private readonly NavMeshAgent _agent;
    private readonly List<Transform> _waypoints;
    private int _currentWaypointIndex;

    public PatrolState(NavMeshAgent agent, List<Transform> waypoints)
    {
        _agent = agent;
        _waypoints = waypoints;
        _currentWaypointIndex = 0;
    }

    public void Enter()
    {
        Debug.Log("Entering Patrol");
        _agent.SetDestination(_waypoints[_currentWaypointIndex].position);
    }

    public void Tick()
    {
        if (_agent.remainingDistance <= 1.5f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count;
            _agent.SetDestination(_waypoints[_currentWaypointIndex].position);
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Patrol");
    }
}
