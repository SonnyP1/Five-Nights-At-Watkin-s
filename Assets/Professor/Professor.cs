using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public enum Door {Right,Left }

public class Professor : MonoBehaviour
{
    [SerializeField] [Range(-1,20)] int _difficultyLevel;
    [SerializeField] int _movementOpportunityFrequency = 4;
    [SerializeField] int _cooldownTime;
    [SerializeField] protected Transform[] _targets;
    [SerializeField] Door _doorLoc;

    [Header("Jump Scare")]
    [SerializeField] GameObject _jumpScareObj;
    [SerializeField] VideoPlayer _jumpScareVid;

    protected Vector3 _startPos;
    private int _targetIndex = 0;
    protected bool _isMoving =false;
    private GameStats _gameStats;
    protected NavMeshAgent _navMeshAgent;
    protected Animator _animator;
    protected bool _isVisable;
    protected Door_Button _door;
    private float _timer;
    protected bool _isCooldownActive = false;

    private void OnBecameVisible()
    {
        _isVisable = true;
    }
    private void OnBecameInvisible()
    {
        _isVisable = false;
    }
    public virtual void Start()
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
            Debug.Log("MOVE AND BE SPOOKY");
            Vector3 currentPos = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
            if (!_isCooldownActive)
            {
                StartCoroutine(CooldownMovement());
                Move();
            }
            else if(_door.GetIsDoorActive() && currentPos == _targets[_targets.Length-2].position)
            {
                Move();
            }
            else
            {
                Debug.Log("COOLING DOWN");
            }
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
        Debug.Log(gameObject.name + " I kill you");
        StartCoroutine(WaitToReloadLevel());
    }
    public virtual void FailMove()
    {
        _targetIndex = 0;
        _navMeshAgent.SetDestination(_startPos);
    }
    virtual public void Update()
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
                if(!_isMoving)
                {
                    MovementOpportunity();
                }
            }
        }
    }

    IEnumerator WaitToReloadLevel()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }

    IEnumerator CooldownMovement()
    {
        _isCooldownActive = true;
        yield return new WaitForSeconds(_cooldownTime);
        _isCooldownActive = false;
    }
}
