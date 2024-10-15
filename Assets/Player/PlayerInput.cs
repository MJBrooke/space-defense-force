using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    
    private Vector2 _movementInput;

    private void Update() => transform.Translate(_movementInput * (moveSpeed * Time.deltaTime));

    private void OnMove(InputValue value) => _movementInput = value.Get<Vector2>();
}
