using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Garcia : Professor
{
    private int successRunCounter;
    private Coroutine checkPathCore;
    private void OnBecameVisible()
    {
        FailMove();
    }
    public override void Move()
    {
        successRunCounter++;
        _animator.SetTrigger("StageIncrease");
        if(successRunCounter == 2)
        {
            _navMeshAgent.SetDestination(_targets[0].position);
            checkPathCore = StartCoroutine(CheckIfPathComplete());
        }
    }
    public override void FailMove()
    {
        successRunCounter = 0;
        _animator.SetTrigger("StageReset");
        _navMeshAgent.SetDestination(_startPos);
    }

    IEnumerator CheckIfPathComplete()
    {
        while(_navMeshAgent.pathStatus != NavMeshPathStatus.PathComplete)
        {
            if (_door.GetIsDoorActive())
            {
                FailMove();
                StopCoroutine(checkPathCore);
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
