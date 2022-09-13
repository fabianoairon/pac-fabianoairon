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

        UpdateGhostPosition(Direction.Up, Vector3.up, ref closestDistance, ref finalDirection);
        UpdateGhostPosition(Direction.Left, Vector3.left, ref closestDistance, ref finalDirection);
        UpdateGhostPosition(Direction.Down, Vector3.down, ref closestDistance, ref finalDirection);
        UpdateGhostPosition(Direction.Right, Vector3.right, ref closestDistance, ref finalDirection);

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

    private void UpdateGhostPosition(Direction direction, Vector3 offset, ref float closestDistance, ref Direction finalDirection)
    {
        if (CheckIfDirectionIsMovable(direction))
        {
            var pacman = GameObject.FindGameObjectWithTag("Player").transform;

            var dist = Vector2.Distance(transform.position + offset, pacman.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                finalDirection = direction;
            }
        }
    }
}
