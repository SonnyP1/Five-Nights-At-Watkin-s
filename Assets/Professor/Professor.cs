using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Professor : MonoBehaviour
{
    [SerializeField] [Range(0,20)] int _difficultyLevel;
    [SerializeField] int _movementOpportunityFrequency = 4;
    [SerializeField] Transform[] _targets;

    private Vector3 _startPos;
    private int _targetIndex = 0;
    private bool isMoving;
    private GameStats _gameStats;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private float _timer;
    void Start()
    {
        _gameStats = FindObjectOfType<GameStats>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _startPos = transform.position;
        StartCoroutine(TimerMovmentActive());
    }

    private void MovementOpportunity()
    {

        int randNum = Random.Range(0,20);
        if(randNum <= _difficultyLevel)
        {
            //do movement
            Debug.Log("MOVE AND BE SPOOKY");
            Move();
        }
    }

    public virtual void Move()
    {
        if(_navMeshAgent.pathStatus != NavMeshPathStatus.PathComplete)
        {
            return;
        }


        if(_targetIndex == _targets.Length-1)
        {
            if(!_gameStats.GetDoorLeft().GetIsDoorActive())
            {
                _navMeshAgent.SetDestination(_targets[_targetIndex].position);
                Debug.Log("JUMP SCARE");
                return;
            }
            else
            {
                FailMove();
                return;
            }
        }

        _navMeshAgent.SetDestination(_targets[_targetIndex].position);
        _targetIndex++;
    }

    public virtual void FailMove()
    {
        _targetIndex = 0;
        _navMeshAgent.SetDestination(_startPos);
    }
    private void Update()
    {
        _animator.SetFloat("Speed",Mathf.Abs(_navMeshAgent.velocity.z));
    }
    IEnumerator TimerMovmentActive()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            _timer += 1;
            if (_timer == _movementOpportunityFrequency)
            {
                _timer = 0;
                MovementOpportunity();
            }
        }
    }
}
