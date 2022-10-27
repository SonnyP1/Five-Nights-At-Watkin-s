using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public enum Door {Right,Left }

public class Professor : MonoBehaviour
{
    [SerializeField] [Range(-1,20)] int _difficultyLevel;
    [SerializeField] int _movementOpportunityFrequency = 4;
    [SerializeField] protected Transform[] _targets;
    [SerializeField] Door _doorLoc;

    [Header("Jump Scare")]
    [SerializeField] GameObject _jumpScareObj;
    [SerializeField] VideoPlayer _jumpScareVid;

    protected Vector3 _startPos;
    private int _targetIndex = 0;
    private bool isMoving;
    private GameStats _gameStats;
    protected NavMeshAgent _navMeshAgent;
    protected Animator _animator;
    protected bool _isVisable;
    protected Door_Button _door;
    private float _timer;

    private void OnBecameVisible()
    {
        _isVisable = true;
    }
    private void OnBecameInvisible()
    {
        _isVisable = false;
    }
    void Start()
    {
        _gameStats = FindObjectOfType<GameStats>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _startPos = transform.position;
        if(_doorLoc == Door.Right)
        {
            _door = _gameStats.GetDoorRight();
        }
        else
        {
            _door = _gameStats.GetDoorLeft();
        }

        StartCoroutine(TimerMovmentActive());
    }

    public void IncreaseDifficultyLevel()
    {
        _difficultyLevel++;
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

        if(_isVisable && _targetIndex == 0)
        {
            return;
        }

        if(_targetIndex == _targets.Length-1)
        {
            if(!_door.GetIsDoorActive())
            {
                _navMeshAgent.SetDestination(_targets[_targetIndex].position);
                JumpScare();
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
    public virtual void JumpScare()
    {
        _gameStats.StopAllAI();
        _jumpScareObj.SetActive(true);
        _jumpScareVid.Play();
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

    public void StopAI()
    {
        StopAllCoroutines();
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
