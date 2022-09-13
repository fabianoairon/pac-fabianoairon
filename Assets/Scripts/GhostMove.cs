using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class GhostMove : MonoBehaviour
{
    CharacterMotor _motor;
    private Vector2 _boxSize;


    void Start()
    {
        _motor = GetComponent<CharacterMotor>();
        _motor.OnAlignedWithGrid += Motor_OnAlignedWithGrid;
        _boxSize = GetComponent<BoxCollider2D>().size;
    }

    private void Motor_OnAlignedWithGrid()
    {
        var pacman = GameObject.FindGameObjectWithTag("Player").transform;

        var closestDistance = float.MaxValue;
        Direction finalDirection = Direction.None;
        if (CheckIfDirectionIsMovable(Direction.Up))
        {
            var dist = Vector2.Distance(transform.position + Vector3.up, pacman.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                finalDirection = Direction.Up;
            }
        }
        if (CheckIfDirectionIsMovable(Direction.Left))
        {
            var dist = Vector2.Distance(transform.position + Vector3.left, pacman.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                finalDirection = Direction.Left;
            }
        }
        if (CheckIfDirectionIsMovable(Direction.Down))
        {
            var dist = Vector2.Distance(transform.position + Vector3.down, pacman.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                finalDirection = Direction.Down;
            }
        }
        if (CheckIfDirectionIsMovable(Direction.Right))
        {
            var dist = Vector2.Distance(transform.position + Vector3.right, pacman.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                finalDirection = Direction.Right;
            }
        }
        _motor.SetMovementDirection(finalDirection);
    }

    private bool CheckIfDirectionIsMovable(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return !Physics2D.BoxCast(transform.position, _boxSize, 0, Vector2.up, 1f, _motor.LevelGatesLayerMask)
                    && _motor.CurrentMoveDirection != Direction.Down;
            case Direction.Left:
                return !Physics2D.BoxCast(transform.position, _boxSize, 0, Vector2.left, 1f, _motor.LevelGatesLayerMask)
                    && _motor.CurrentMoveDirection != Direction.Right;
            case Direction.Down:
                return !Physics2D.BoxCast(transform.position, _boxSize, 0, Vector2.down, 1f, _motor.LevelGatesLayerMask)
                    && _motor.CurrentMoveDirection != Direction.Up;
            case Direction.Right:
                return !Physics2D.BoxCast(transform.position, _boxSize, 0, Vector2.right, 1f, _motor.LevelGatesLayerMask)
                    && _motor.CurrentMoveDirection != Direction.Left;
        }
        return false;
    }
}
