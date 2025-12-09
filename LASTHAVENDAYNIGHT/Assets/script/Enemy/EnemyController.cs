using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        
        // Can 0 veya altına düşerse düşmanı yok et
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Burada patlama efekti vs. ekleyebilirsin
        Destroy(gameObject);
    }
}