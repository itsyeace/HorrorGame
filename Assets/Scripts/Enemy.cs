using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _playerTransform;
    private IEnemyState _currentState;
    [SerializeField] private List<Transform> waypoints;
    
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float searchDuration = 5f;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        PlayerCharacter player = FindAnyObjectByType<PlayerCharacter>();
        _playerTransform = player.transform;
        _currentState = new PatrolState(_agent, waypoints);
        _currentState.Enter();
    }

    void Update()
    {
        _currentState.Tick();
        
        bool canSeePlayer = CanSeePlayer();

        if (canSeePlayer && !(_currentState is ChaseState))
        {
            SwitchState(new ChaseState(_agent, _playerTransform));
        }

        if (!canSeePlayer && (_currentState is ChaseState))
        {
            var searchState = new SearchState(_agent, _playerTransform.position, searchDuration);
            searchState.OnSearchComplete += HandleSearchComplete;
            SwitchState(searchState);
        }
            
        else if (!canSeePlayer && !(_currentState is PatrolState) && !(_currentState is SearchState))
        {
            SwitchState(new PatrolState(_agent, waypoints));
        }
    }

    void SwitchState(IEnemyState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    bool CanSeePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);

        if (distanceToPlayer <= detectionRange)
        {
            Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;
            Ray ray = new Ray(transform.position, directionToPlayer);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, detectionRange))
            {
                if (hit.collider.gameObject == _playerTransform.gameObject)
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red, 5f);
                    return true;
                }
            }
        }
        return false;
    }

    void HandleSearchComplete()
    {
        SwitchState(new PatrolState(_agent, waypoints));
    }
}



