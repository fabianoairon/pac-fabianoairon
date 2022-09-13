using System;
using UnityEngine;
public enum Direction
{
    None,
    Up,
    Left,
    Down,
    Right
}

public class CharacterMotor : MonoBehaviour
{
    public float MoveSpeed;

    public event Action<Direction> OnDirectionChanged;

    public event Action OnAlignedWithGrid;

    public LayerMask LevelGatesLayerMask;

    public Direction CurrentMoveDirection
    {
        get
        {
            // up
            if (_currentMoveDirection.y > 0)
            {
                return Direction.Up;
            }

            // left
            if (_currentMoveDirection.x < 0)
            {
                return Direction.Left;
            }

            // down
            if (_currentMoveDirection.y < 0)
            {
                return Direction.Down;
            }

            // right
            if (_currentMoveDirection.x > 0)
            {
                return Direction.Right;
            }

            return Direction.None;
        }
    }

    private Rigidbody2D _rigidBody;
    private Vector2 _desiredMoveDirection;
    private Vector2 _currentMoveDirection;


    private Vector2 _boxSize;

    public void SetMovementDirection(Direction newMoveDirection)
    {
        switch (newMoveDirection)
        {
            default:
            case Direction.None:
                _desiredMoveDirection = Vector2.zero;
                break;
            case Direction.Up:
                _desiredMoveDirection = Vector2.up;
                break;
            case Direction.Left:
                _desiredMoveDirection = Vector2.left;
                break;
            case Direction.Down:
                _desiredMoveDirection = Vector2.down;
                break;
            case Direction.Right:
                _desiredMoveDirection = Vector2.right;
                break;
        }
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxSize = GetComponent<BoxCollider2D>().size;
        LevelGatesLayerMask = LayerMask.GetMask(new string[] { "Level", "Gates" });
    }

    private void FixedUpdate()
    {
        float moveDistance = MoveSpeed * Time.fixedDeltaTime;
        var nextMovePosition = _rigidBody.position + _currentMoveDirection * moveDistance;

        // up
        if (_currentMoveDirection.y > 0)
        {
            var maxY = Mathf.CeilToInt(_rigidBody.position.y);
            if (nextMovePosition.y >= maxY)
            {
                transform.position = new Vector2(_rigidBody.position.x, maxY);
                //moveDistance = nextMovePosition.y - maxY;
            }
        }

        // left
        if (_currentMoveDirection.x < 0)
        {
            var minX = Mathf.FloorToInt(_rigidBody.position.x);
            if (nextMovePosition.x <= minX)
            {
                transform.position = new Vector2(minX, _rigidBody.position.y);
                //moveDistance = minX - nextMovePosition.x;
            }
        }

        // down
        if (_currentMoveDirection.y < 0)
        {
            var minY = Mathf.FloorToInt(_rigidBody.position.y);
            if (nextMovePosition.y <= minY)
            {
                transform.position = new Vector2(_rigidBody.position.x, minY);
                //moveDistance = minY - nextMovePosition.y;
            }
        }

        // right
        if (_currentMoveDirection.x > 0)
        {
            var maxX = Mathf.CeilToInt(_rigidBody.position.x);
            if (nextMovePosition.x >= maxX)
            {
                transform.position = new Vector2(maxX, _rigidBody.position.y);
                //moveDistance = nextMovePosition.x - maxX;
            }
        }

        Physics2D.SyncTransforms();

        if (_rigidBody.position.y == Mathf.CeilToInt(_rigidBody.position.y) && _rigidBody.position.x == Mathf.CeilToInt(_rigidBody.position.x) || _currentMoveDirection == Vector2.zero)
        {
            OnAlignedWithGrid?.Invoke();
            if (_currentMoveDirection != _desiredMoveDirection)
            {
                if (!Physics2D.BoxCast(_rigidBody.position, _boxSize, 0, _desiredMoveDirection, 1f, LevelGatesLayerMask))
                {
                    _currentMoveDirection = _desiredMoveDirection;
                    OnDirectionChanged?.Invoke(CurrentMoveDirection);
                }
            }

            if (Physics2D.BoxCast(_rigidBody.position, _boxSize, 0, _currentMoveDirection, 1f, LevelGatesLayerMask))
            {
                _currentMoveDirection = Vector2.zero;
                OnDirectionChanged?.Invoke(CurrentMoveDirection);
            }
        }

        _rigidBody.MovePosition(_rigidBody.position + _currentMoveDirection * moveDistance);
    }
}
