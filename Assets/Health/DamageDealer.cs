using UnityEngine;

/// <summary>
/// OnCollision, if the object being collided with has a Health component, deal damage to it
/// </summary>
public class DamageDealer : MonoBehaviour
{
    [Tooltip("The amount of damage done to the Health component of the other GameObject that this GameObject collides with")]
    [SerializeField] private float damage = 10f;
    
    // If the owning GameObject collides with another GameObject, deal damage to it if it is damageable.
    // If damage is dealt, destroy the owning GameObject.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<IDamageable>(out var damageable)) return;
        
        damageable.TakeDamage(damage);
    }
}