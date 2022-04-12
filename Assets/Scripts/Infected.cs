using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Infected : BedList
{
    [SerializeField] private State BedState;
    [SerializeField] private State RunState;
    [SerializeField] private State RunExitState;
    

    [SerializeField] private float _time;
    [SerializeField] private Material _normalSkin;
    [SerializeField] private List<Material> _skins = new List<Material>();
    [SerializeField] private List<String> _tags = new List<String>();
    
    private bool _goExit;
    private bool _readyTreatment;
    private Animator _myAnimator;
    private NavMeshAgent _navMeshAgent;
    private SkinnedMeshRenderer _skin;
    private PoolObject _currentPoolObject;
    private int _index;


    [Header("Actual State")]
    public State CurrentState;

    [HideInInspector] public Vector3 CurrentBed;

    private float _angleSpeed = 120f;


    private void Start()
    {
        _myAnimator = GetComponent<Animator>();
        _currentPoolObject = GetComponent<PoolObject>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _skin = GetComponentInChildren<SkinnedMeshRenderer>();
        _readyTreatment = false;
        _goExit = false;
        _skin.material = GetSkin();
        gameObject.tag = GetTag();
        SetState(RunState);
    }

    void OnEnable()
    {
        _skin.material = GetSkin();
        gameObject.tag = GetTag();
        SetState(RunState);
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
        if (other.CompareTag(gameObject.tag) && _readyTreatment)
        {
            RunToExit();
            other.tag = "Player";
            SyringeTable.SyringeTakenEvent.Invoke(null);
        }
    }

    public void SetState(State state)
    {
        CurrentState = Instantiate(state);
        CurrentState.Infected = this;
        CurrentState.Init();
    }
    
    public void MoveTo(Vector3 TargetPosition)
    {
        CurrentBed = TargetPosition;
        
        TargetPosition.y = transform.position.y;

        MoveAnimationSpeed(TargetPosition.magnitude);

        _navMeshAgent.SetDestination(TargetPosition);
    }

    public void IsComeToBed(Vector3 BedPosition)
    {
        var distance = (BedPosition - transform.position).magnitude;
        if (distance < 1.9f && !_readyTreatment)
        {
            _myAnimator.SetBool("Sleep", true);
            _readyTreatment = true;
            _navMeshAgent.enabled = false;
            SetState(BedState);
        }
    }

    public void IsComeToExit(Vector3 ExitPosition)
    {
        var distance = (ExitPosition - transform.position).magnitude;
        if (distance < 1.9f && _goExit)
        {
            _currentPoolObject.ReturnToPool();
            _readyTreatment = false;
            _goExit = false;
        }
    }

    public Vector3 FoundBedInfected()
    {
        return FoundBed();
    }

    protected void RunToExit()
    {
        _skin.material = _normalSkin;
        _myAnimator.SetBool("Sleep", false);
        _navMeshAgent.enabled = true;
        SetState(RunExitState);
        _goExit = true;
        BedIsFree();
    }

    private Material GetSkin()
    {
        _index = Random.Range(0, _skins.Count);

        return _skins[_index];
    }

    private String GetTag()
    {
        return _tags[_index];
    }

    protected void MoveAnimationSpeed(float speed)
    {
        _myAnimator.SetFloat("Speed", speed);
    }

    public void StartTimer()
    {
        StartCoroutine(WaitBed());
    }
    private IEnumerator WaitBed()
    {
        yield return new WaitForSeconds(_time);
        SetState(RunState);
        StartCoroutine(WaitBed());
    }
}
