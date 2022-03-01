using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public State BedState;
    public State WaitForRunState;
    public State RunState;
    public Animator EnemyAnimator;

    [Header("Actual State")]
    public State CurrentState;

    private float _angleSpeed = 120f;

    private void Start()
    {
        SetState(WaitForRunState);
        EnemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!CurrentState.IsFinished)
        {
            CurrentState.Do();
        }
        else
        {
            if (BedList.EmptyBeds.Count <= 0)
            {
                SetState(WaitForRunState);
            }
            else if (BedList.EmptyBeds.Count > 0)
            {
                SetState(RunState);
            }
        }
    }

    public void SetState(State state)
    {
        CurrentState = Instantiate(state);
        CurrentState.Enemy = this;
        CurrentState.Init();
    }
    
    public void MoveTo(Vector3 BedPosition)
    {
        BedPosition.y = transform.position.y;

        MoveAnimationSpeed(BedPosition.magnitude);

        transform.position = Vector3.MoveTowards(transform.position, BedPosition, Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(BedPosition - transform.position), Time.deltaTime * _angleSpeed);
        
        var distance = (BedPosition - transform.position).magnitude;

        if (distance < 1f)
        {
            SetState(BedState);
        }
    }

    private void MoveAnimationSpeed(float speed)
    {
        EnemyAnimator.SetFloat("Speed", speed);
    }
}
