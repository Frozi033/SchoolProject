using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private State BedState;
    [SerializeField] private State RunState;
    [SerializeField] private State RunExitState;
    public Animator EnemyAnimator { get; private set; } 
    

    [Header("Actual State")]
    public State CurrentState;

    private float _angleSpeed = 120f;

    private void Start()
    {
        SetState(RunState);
        EnemyAnimator = GetComponent<Animator>();
        SyringeObject.PatientIsHealthy += RunToExit;
    }

    private void Update()
    {
        if (!CurrentState.IsFinished)
        {
            CurrentState.Do();
        }
        /*else
        {
            if (BedList.EmptyBeds.Count <= 0)
            {
                SetState(WaitForRunState);
            }
            else if (BedList.EmptyBeds.Count > 0)
            {
                SetState(RunState);
            }
        }*/
    }

    public void SetState(State state)
    {
        CurrentState = Instantiate(state);
        CurrentState.Enemy = this;
        CurrentState.Init();
    }
    
    public void MoveTo(Vector3 TargetPosition)
    {
        TargetPosition.y = transform.position.y;

        MoveAnimationSpeed(TargetPosition.magnitude);

        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(TargetPosition - transform.position), Time.deltaTime * _angleSpeed);
        
        var distance = (TargetPosition - transform.position).magnitude;

        if (distance < 1f)
        {
            SetState(BedState);
        }
    }

    private void RunToExit()
    {
        SetState(RunExitState);
    }
    

    private void MoveAnimationSpeed(float speed)
    {
        //EnemyAnimator.SetFloat("Speed", speed);
    }
}
