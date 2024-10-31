using UnityEngine;

/// <summary>
/// OnCollision, if the object being collided with has a Health component, deal damage to it and play associated effects
/// </summary>
public class DamageDealer : MonoBehaviour
{
    // TODO - use inheritance to create a separate DamageDealer for the Player and the Enemy, rather than using tags
    
    [Tooltip("The amount of damage done to the Health component of the other GameObject that this GameObject collides with")]
    [SerializeField] private float damage = 10f;
    
    private ParticleSystem _damageParticles;

    private void Start() => _damageParticles = Resources.Load<ParticleSystem>("Projectile Hit Particles");

    // If the owning GameObject collides with another GameObject, deal damage to it if it is damageable.
    // If damage is dealt, destroy the owning GameObject.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<IDamageable>(out var damageable)) return;
        
        damageable.TakeDamage(damage);
        PlayParticles();
        ShakeCamera(other);
    }

    private void ShakeCamera(Collider2D other)
    {
        // We could also achieve this with a Serialized field
        if (other.gameObject.CompareTag("Player")) CameraShake.Instance.Play();
    }

    private void PlayParticles()
    {
        var particles = Instantiate(_damageParticles, transform.position, Quaternion.identity);
        particles.Play();
        Destroy(particles.gameObject, particles.main.duration);
    }
}