using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private State BedState;
    [SerializeField] private State RunState;
    [SerializeField] private State RunExitState; 
    
    private static Pool _pool;
    private PoolObject _currentPoolObject;
    private int _currentId;
    private bool _goExit;
    private StickmanCore.Status _syringeTaken;
    private bool _readyTreatment;
    private Animator _myAnimator; 
    

    [Header("Actual State")]
    public State CurrentState;

    private float _angleSpeed = 120f;
    
    public static Action<int, string> TreatmentEvent;

    private void OnEnable()
    {
        _myAnimator = GetComponent<Animator>();
        _currentPoolObject = GetComponent<PoolObject>();
        _syringeTaken = StickmanCore.Status.SyringeTaken;
        _currentId = SpawnPoint.idInfected;
        Debug.Log(_currentId + "CurrentID");
        SetState(RunState);
        SyringeObject.PatientIsHealthy += RunToExit;
    }

    private void Update()
    {
        if (!CurrentState.IsFinished)
        {
            CurrentState.Do();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _readyTreatment)// && StickmanCore.LifeStatus == _syringeTaken)
        {
            TreatmentEvent.Invoke(_currentId, tag);
            Debug.Log("Cearam");
        }
    }

    public void SetState(State state)
    {
        CurrentState = Instantiate(state);
        CurrentState.Enemy = this;
        CurrentState.Init(id:_currentId);
        Debug.Log("setState");
    }
    
    public void MoveTo(Vector3 TargetPosition)
    {
        TargetPosition.y = transform.position.y;

        MoveAnimationSpeed(TargetPosition.magnitude);

        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(TargetPosition - transform.position), Time.deltaTime * _angleSpeed);
        
        var distance = (TargetPosition - transform.position).magnitude;
        Debug.Log(CurrentState);

        if (distance < 1f && !_readyTreatment)
        {
            Debug.Log("Booom");
            _readyTreatment = true;
            _myAnimator.SetBool("Sleep", true);
            SetState(BedState);
        }
        else if (distance < 1f && _goExit)
        {
            _currentPoolObject.ReturnToPool();
        }
    }

    private void RunToExit(int id)
    {
        if (id == _currentId)
        {
            _myAnimator.SetBool("Sleep", false);
            SetState(RunExitState);
            _goExit = true;
        }
    }
    

    private void MoveAnimationSpeed(float speed)
    {
        _myAnimator.SetFloat("Speed", speed);
    }
}
