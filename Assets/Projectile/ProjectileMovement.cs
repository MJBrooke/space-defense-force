using System;
using UnityEngine;

/// <summary>
/// Moves a projectile across the screen vertically at the configured speed
/// </summary>
public class ProjectileMovement : MonoBehaviour
{
    private enum Direction
    {
        Up,
        Down
    }

    [Tooltip("Speed at which the projectile travels")]
    [SerializeField] private float movementSpeed = 15f;

    [Tooltip("Direction the projectile moves")]
    [SerializeField] private Direction direction;

    private void Update() => transform.position += TransformDirection() * (movementSpeed * Time.deltaTime);

    private Vector3 TransformDirection() =>
        direction switch
        {
            Direction.Up => transform.up,
            Direction.Down => transform.up * -1,
            _ => throw new ArgumentOutOfRangeException()
        };
}