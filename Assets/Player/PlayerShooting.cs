using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles Player shooting by listening to the PlayerInput's UnityEvent and toggling shooting in the Shooter component
/// </summary>
[RequireComponent(typeof(Shooting))]
public class PlayerShooting : MonoBehaviour
{
    private Shooting _shooterScript;

    private void Start() => _shooterScript = GetComponent<Shooting>();

    // Assigned to the PlayerInput's 'Fire' UnityEvent Callback
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed) _shooterScript.isFiring = true;
        else if (context.canceled) _shooterScript.isFiring = false;
    }
}