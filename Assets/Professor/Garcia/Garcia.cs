using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Garcia : Professor
{
    //audio1 is running sound
    //audio2 is banging sound
    [SerializeField] CinemachineVirtualCamera RunCam;
    private int successRunCounter;
    private IEnumerator check;
    private SkinnedMeshRenderer _meshRenderer;

    public override void Start()
    {
        base.Start();
        check = CheckIfPathComplete();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    public override void Move()
    {
        if(_gameStats.GetUIManager().GetInTablet())
        {
            return;
        }
        successRunCounter++;
        Debug.Log(successRunCounter);
        _animator.SetFloat("Stages",successRunCounter);
        if(successRunCounter == 3)
        {
            _navMeshAgent.SetDestination(_targets[0].position);
            _meshRenderer.enabled = false;
            StartCoroutine(check);
        }
    }
    public override void FailMove()
    {
        StopCoroutine(check);
        check = CheckIfPathComplete();
        successRunCounter = 0;
        Debug.Log(successRunCounter);
        _animator.SetFloat("Stages", successRunCounter);


        _meshRenderer.enabled = true;
        _navMeshAgent.SetDestination(_startPos);

        StartCoroutine(RotToOringalRot(1.0f));
        _isMoving = false;
    }

    public override void Update()
    {
    }

    IEnumerator CheckIfPathComplete()
    {
        _isMoving = true;
        int timerToRunDownHall = 0;
        while(true)
        {
            yield return new WaitForSeconds(1);
            timerToRunDownHall++;
            if(_gameStats.GetUIManager().GetCurrentCam() == RunCam)
            {
                _meshRenderer.enabled = true;
                _navMeshAgent.SetDestination(_targets[1].position);
                break;
            }
            if(timerToRunDownHall == 20)
            {
                _meshRenderer.enabled = true;
                _navMeshAgent.SetDestination(_targets[1].position);
                break;
            }
        }

        _audio1.Play();
        while (gameObject.transform.position != _targets[1].position)
        {
            yield return new WaitForEndOfFrame();
        }
        CheckDoor:
        if (_door.GetIsDoorActive())
        {
            _audio1.Stop();
            _audio2.Play();
            while(_audio2.isPlaying)
            {
                if(!_door.GetIsDoorActive())
                {
                    goto CheckDoor;
                }
                yield return new WaitForEndOfFrame();
            }

            FailMove();
        }
        else
        {
            _navMeshAgent.SetDestination(_targets[2].position);
            while(gameObject.transform.position != _targets[2].position)
            {
                yield return new WaitForEndOfFrame();
            }


            JumpScare();
        }
    }
}
