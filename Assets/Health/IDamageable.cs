/// <summary>
/// Specifies that a particular GameObject can be damaged, such as the player or a destructible environmental object
/// </summary>
public interface IDamageable
{
    public void TakeDamage(float damage);
}
