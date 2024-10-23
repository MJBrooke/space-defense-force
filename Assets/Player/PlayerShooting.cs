using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles Player shooting by listening to the PlayerInput's UnityEvent and firing at a configured rate per second
/// </summary>
public class PlayerShooting : MonoBehaviour
{
    [Tooltip("How many bullets will be fired per second")]
    [SerializeField] private float fireRate = 0.5f;

    // Stores the PlayerInput value indicating if the Player is shooting or not
    private bool _isFiring = false;
    
    // Used to track DeltaTime to ensure that we are firing at the desired rate
    private float _fireTimer = 0f;

    private void Update()
    {
        if (_isFiring && _fireTimer <= 0f)
        {
            Debug.Log("Firing!");
            _fireTimer = fireRate; // Restart the timer
        }

        // Track time elapsed in the frame
        if (_fireTimer > 0f) _fireTimer -= Time.deltaTime;
    }

    // Assigned to the PlayerInput's 'Fire' UnityEvent Callback
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed) _isFiring = true;
        else if (context.canceled) _isFiring = false;
    }
}