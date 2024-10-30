using UnityEngine;

/// <summary>
/// Continuously fires projectiles at a configured rate-per-second, controlled by an 'isFiring' flag
/// </summary>
public class Shooting : MonoBehaviour
{
    [Tooltip("The projectile to be fired")]
    [SerializeField] private GameObject projectilePrefab;
    
    [Tooltip("How many bullets will be fired per second")]
    [SerializeField] private float fireRate = 0.25f;
    
    [Tooltip("Toggles firing on or off")]
    [SerializeField] public bool isFiring;
    
    // Used to track DeltaTime to ensure that we are firing at the desired rate
    private float _fireTimer = 0f;

    private void Update() => ContinuouslyShoot();

    private void ContinuouslyShoot()
    {
        if (isFiring && _fireTimer <= 0f)
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
}
