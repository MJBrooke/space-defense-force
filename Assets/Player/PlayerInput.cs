using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 _movementInput;
    private Vector2 _bottomLeftCorner;
    private Vector2 _topRightCorner;

    private void Start()
    {
        if (Camera.main == null)
        {
            Debug.LogError("No main camera found");
            return;
        }
        
        _bottomLeftCorner = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        _topRightCorner = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void Update()
    {
        if (_movementInput is { x: 0, y: 0 }) return;

        var rawMovementForFrame = _movementInput * (moveSpeed * Time.deltaTime);
        
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x + rawMovementForFrame.x, _bottomLeftCorner.x, _topRightCorner.x),
            Mathf.Clamp(transform.position.y + rawMovementForFrame.y, _bottomLeftCorner.y, _topRightCorner.y));
    }

    private void OnMove(InputValue value) => _movementInput = value.Get<Vector2>();
}