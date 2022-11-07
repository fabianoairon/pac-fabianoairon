using UnityEngine;

public class PacManVisuals : MonoBehaviour
{

    public Life CharacterLife;
    public CharacterMotor Motor;

    public AudioSource AudioSource;
    public AudioClip AudioClip;

    private Animator _animator;




    void Start()
    {
        _animator = GetComponent<Animator>();
        Motor.OnDirectionChanged += _motor_OnDirectionChanged;
        CharacterLife.OnRemovedLife += CharacterLife_OnRemovedLife;
        Motor.OnResetPosition += Motor_OnResetPosition;
        GameManager.OnVictory += GameManager_OnVictory;
        _animator.speed = 1;
    }

    private void GameManager_OnVictory()
    {
        if (_animator == null) return;
        _animator.speed = 0;
    }

    private void Motor_OnResetPosition()
    {
        _animator.SetBool("Dead", false);

    }

    private void CharacterLife_OnRemovedLife(int _)
    {
        transform.Rotate(0, 0, -90);
        AudioSource.PlayOneShot(AudioClip);
        _animator.SetBool("Moving", false);
        _animator.SetBool("Dead", true);
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
