using UnityEngine;

public class Player : StickmanCore
{
    [SerializeField] private FloatingJoystick _joystick;
    void Update()
    {
        Move();
    }
    private void Move()
    {
        transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);

        Vector3 direction = new Vector3(_joystick.Vertical, 0, _joystick.Horizontal * -1);

        Move(direction);
    }
}
