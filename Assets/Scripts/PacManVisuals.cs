using UnityEngine;

public class PacManVisuals : MonoBehaviour
{
    public CharacterMotor Motor;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        Motor.OnDirectionChanged += _motor_OnDirectionChanged;
    }

    private void _motor_OnDirectionChanged(Direction direction)
    {
        switch (direction)
        {
            default:
            case Direction.None:
                _animator.SetBool("Moving", false);
                break;
            case Direction.Up:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                _animator.SetBool("Moving", true);
                break;
            case Direction.Left:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                _animator.SetBool("Moving", true);
                break;
            case Direction.Down:
                transform.rotation = Quaternion.Euler(0, 0, 270);
                _animator.SetBool("Moving", true);
                break;
            case Direction.Right:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _animator.SetBool("Moving", true);
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
