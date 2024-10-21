using UnityEngine;

/// <summary>
/// Handles logic for tracking and mutating a GameObject's health
/// </summary>
public class Health : MonoBehaviour, IDamageable
{
    [Tooltip("The starting health of this GameObject")]
    [SerializeField] private float maxHealth = 100f;
    
    private float _currentHealth;

    private void Start() => _currentHealth = maxHealth;

    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Max(_currentHealth - damage, 0f);
        if (_currentHealth == 0f) Die();
    }
    
    private void Die() => Destroy(gameObject);
}
