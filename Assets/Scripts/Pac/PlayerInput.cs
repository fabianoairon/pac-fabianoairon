using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class PlayerInput : MonoBehaviour
{
    private CharacterMotor _motor;

    private void Start()
    {
        _motor = GetComponent<CharacterMotor>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _motor.SetMovementDirection(Direction.Up);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _motor.SetMovementDirection(Direction.Left);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _motor.SetMovementDirection(Direction.Down);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _motor.SetMovementDirection(Direction.Right);
        }
    }

}
