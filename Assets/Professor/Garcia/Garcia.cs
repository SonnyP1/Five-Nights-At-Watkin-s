using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Garcia : Professor
{
    private int successRunCounter;
    private IEnumerator check;
    private SkinnedMeshRenderer _meshRenderer;

    public override void Start()
    {
        base.Start();
        check = CheckIfPathComplete();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    private void OnBecameVisible()
    {
        //FailMove();
    }
    public override void Move()
    {
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
            if(timerToRunDownHall == 5)
            {
                _meshRenderer.enabled = true;
                _navMeshAgent.SetDestination(_targets[1].position);
                break;
            }
        }

        while(gameObject.transform.position != _targets[1].position)
        {
            yield return new WaitForEndOfFrame();
        }

        if (_door.GetIsDoorActive())
        {
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
