using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IEnemyState
{
    private readonly NavMeshAgent _agent;
    private readonly Transform _playerTransform;

    public ChaseState(NavMeshAgent agent, Transform playerTransform)
    {
        _agent = agent;
        _playerTransform = playerTransform;
    }

    public void Enter()
    {
        Debug.Log("Entering Chase");
    }

    public void Tick()
    {
        _agent.SetDestination(_playerTransform.position);
    }

    public void Exit()
    {
        Debug.Log("Exiting Chase");
    }
}
