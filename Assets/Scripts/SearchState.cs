using UnityEngine;
using UnityEngine.AI;
using System;

public class SearchState : IEnemyState
{
    private readonly NavMeshAgent _agent;
    private readonly Vector3 _lastKnownPosition;
    private readonly float _searchDuration;
    
    private float _elapsedTime = 0f;

    public event Action OnSearchComplete;
    
    public SearchState(NavMeshAgent agent, Vector3 lastKnownPosition, float searchDuration)
    {
        _agent = agent;
        _lastKnownPosition = lastKnownPosition;
        _searchDuration = searchDuration;
    }

    public void Enter()
    {
        Debug.Log("Search Started!");
        _agent.SetDestination(_lastKnownPosition);
    }

    public void Tick()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= _searchDuration)
        {
            OnSearchComplete?.Invoke();
            _elapsedTime = 0f;
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Search!");
    }
}