using UnityEngine;

public class BreakableProps : MonoBehaviour
{
    private float _health;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
