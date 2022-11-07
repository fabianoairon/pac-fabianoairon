using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class GhostMove : MonoBehaviour
{
    public event Action OnChangeTarget;

    private CharacterMotor _motor;
    private Vector2 _boxSize;
    private Vector2 _targetPosition;

    public void SetTargetPosition(Vector2 targetPos)
    {
        _targetPosition = targetPos;
    }

    public void ResetPos()
    {
        _motor.ResetPosition();
    }

    public void StopMoving()
    {
        _motor.enabled = false;
    }

    public void StartMoving()
    {
        _motor.enabled = true;
    }

    void Start()
    {
        _motor = GetComponent<CharacterMotor>();
        _motor.OnAlignedWithGrid += Motor_OnAlignedWithGrid;
        _boxSize = GetComponent<BoxCollider2D>().size;
    }

    private void Motor_OnAlignedWithGrid()
    {
        ChangeDirection();
        OnChangeTarget?.Invoke();
    }
    private void ChangeDirection()
    {
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
            var dist = Vector2.Distance(transform.position + offset, _targetPosition);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                finalDirection = direction;
            }
        }
    }
}
