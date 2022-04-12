using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class StickmanCore : MonoBehaviour
{
    private GameObject _currentSyringe;
    
    private CharacterController _myController;
    private Vector3 _velocity;
    
    private float _gravityValue = -10f;
    private float _speed = 10f;
    private bool _syringe;
    private Animator _myAnimator;
    private Vector3 _playerVelocityY;

    public static Status LifeStatus { get; protected internal set; }


    public virtual void Awake()
    {
        _myAnimator = GetComponent<Animator>();
        _myController = GetComponent<CharacterController>();

        SyringeTable.SyringeTakenEvent += SyringeSwitch;
        _syringe = false;
        
        LifeStatus = Status.Live;
    }
    

    protected void Move(Vector3 direction)
    {
        _myController.Move(direction * _speed * Time.deltaTime);
        _myController.Move(_playerVelocityY * Time.deltaTime);

        MoveAnimationSpeed(direction.magnitude);
        
        Gravity();

        if (direction != Vector3.zero)
            transform.forward = Vector3.SmoothDamp(transform.forward, direction, ref _velocity, 1f * Time.deltaTime);
    }
    
    private void Gravity()
    {
        if (!_myController.isGrounded)
        {
            _playerVelocityY.y += _gravityValue * Time.deltaTime;

        }
        else
        {
            _playerVelocityY.y = 0f;
        }
    }

    private void MoveAnimationSpeed(float speed)
    {
        _myAnimator.SetFloat("Speed", speed);
    }

    private void SyringeSwitch(GameObject Syringe)
    {
        if (Syringe != null)
        {
            _currentSyringe = Syringe;
            _syringe = !_syringe;
            _myAnimator.SetBool("Syringe", _syringe);
            Syringe.SetActive(_syringe);
        }
        else
        {
            _syringe = !_syringe;
            _myAnimator.SetBool("Syringe", _syringe);
            _currentSyringe.SetActive(_syringe);
        }
    }
    
    public enum Status
    {
        Live
    }
}

