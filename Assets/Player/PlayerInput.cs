using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles receiving Player movement input and translating that into movement within the game.
/// It ensures that the Player can never move out of bounds of the Viewport.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class PlayerInput : MonoBehaviour
{
    [Tooltip ("The speed at which the player moves")]
    [SerializeField] private float moveSpeed = 8f;

    private Collider2D _playerCollider;
    
    // Stores raw movement input values from the InputSystem, making it available across the class
    private Vector2 _movementInput; 
    
    // World-space co-ordinates of the Camera's Viewport
    private Vector2 _bottomLeftCorner; 
    private Vector2 _topRightCorner;

    private void Start()
    {
        // We cannot bind the player to the Viewport without there being a Camera in the Scene.
        if (Camera.main == null)
        {
            Debug.LogError("No main camera found");
            return;
        }
        
        _playerCollider = GetComponent<Collider2D>();
        
        // We get the world-space co-ordinates of the corners of the Viewport.
        // This will allow us to create a boundary for the Player's ship that it cannot pass.
        // We add some padding by adding/subtracting half the Player's height to ensure the full ship stays in the bounds.
        var halfPlayerDimension = _playerCollider.bounds.extents;
        _bottomLeftCorner = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)) + halfPlayerDimension;
        _topRightCorner = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) - halfPlayerDimension;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!IsMoving()) return;

        // Since we're directly assigning the Position rather than using Velocity, we need to factor in DeltaTime
        // for framerate agnostic movement speed.
        var rawMovementForFrame = _movementInput * (moveSpeed * Time.deltaTime);
        
        // We ensure that the movement can never move past the bounds by Clamping the values for x and y based on the Viewport
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x + rawMovementForFrame.x, _bottomLeftCorner.x, _topRightCorner.x),
            Mathf.Clamp(transform.position.y + rawMovementForFrame.y, _bottomLeftCorner.y, _topRightCorner.y));
    }

    // OnMove is automatically fired off when 'Move' input is detected by the Input System.
    private void OnMove(InputValue value) => _movementInput = value.Get<Vector2>();
    
    private bool IsMoving() => _movementInput != Vector2.zero;
}