using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles Player shooting by listening to the PlayerInput's UnityEvent and firing at a configured rate per second
/// </summary>
public class PlayerShooting : MonoBehaviour
{
    [Tooltip("How many bullets will be fired per second")]
    [SerializeField] private float fireRate = 0.25f;
    [SerializeField] private GameObject projectilePrefab;

    // Stores the PlayerInput value indicating if the Player is shooting or not
    private bool _isFiring = false;
    
    // Used to track DeltaTime to ensure that we are firing at the desired rate
    private float _fireTimer = 0f;

    private void Update() => ManageFiring();

    private void ManageFiring()
    {
        if (_isFiring && _fireTimer <= 0f)
        {
            ShootProjectile();
            _fireTimer = fireRate; // Restart the timer
        }

        // Track time elapsed in the frame
        if (_fireTimer > 0f) _fireTimer -= Time.deltaTime;
    }

    private void ShootProjectile()
    {
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Destroy(projectile, 3f); // TODO - better to kill the projectile when off-screen rather? This is fairly basic...
    }

    // Assigned to the PlayerInput's 'Fire' UnityEvent Callback
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed) _isFiring = true;
        else if (context.canceled) _isFiring = false;
    }
}