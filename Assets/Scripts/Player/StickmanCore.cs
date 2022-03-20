using System;
using UnityEngine;

public class StickmanCore : MonoBehaviour
{
    private CharacterController _myController;
    private Rigidbody[] _bodyRigidbodies;
    private Vector3 _velocity;

    private float _mySpeed = 10f;
    private Animator _myAnimator;
    
    public static Status LifeStatus { get; protected set; }

    public static Action<GameObject> onSyringeTreatmentEvent;
    

    public virtual void Awake()
    {
        _myAnimator = GetComponent<Animator>();
        _bodyRigidbodies = GetComponentsInChildren<Rigidbody>();
        _myController = GetComponent<CharacterController>();
        
        LifeStatus = Status.Live;

        SetRagdollActive(false);
        ChangeMass(20f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("PurpleInfected") || other.CompareTag("GreenInfected")) && LifeStatus == Status.SyringeTaken)
        {
            onSyringeTreatmentEvent.Invoke(other.gameObject);
            //Debug.Log("Cearam");
        }
    }

    protected void Move(Vector3 direction)
    {
        _myController.Move(direction * Time.deltaTime * _mySpeed);

        MoveAnimationSpeed(direction.magnitude);

        if (direction != Vector3.zero)
            transform.forward = Vector3.SmoothDamp(transform.forward, direction, ref _velocity, 1f * Time.deltaTime);
    }

    protected void SetRagdollActive(bool active)
    {
        for (int i = 0; i < _bodyRigidbodies.Length; i++)
            _bodyRigidbodies[i].isKinematic = !active;
    }

    private void ChangeMass(float mult)
    {
        for (int i = 0; i < _bodyRigidbodies.Length; i++)
            _bodyRigidbodies[i].mass *= mult;
    }

    protected void MoveAnimationSpeed(float speed)
    {
        _myAnimator.SetFloat("Speed", speed);
    }
    
    public enum Status
    {
        Live,
        SyringeTaken
    }
}

