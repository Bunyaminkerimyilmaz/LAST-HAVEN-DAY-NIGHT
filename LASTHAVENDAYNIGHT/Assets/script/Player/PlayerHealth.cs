using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Oyuncu Canı: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Oyuncu Öldü!");
        // Burada oyun bitiş ekranını açabilir veya sahneyi yeniden başlatabilirsin.
        // Time.timeScale = 0; // Oyunu durdurur
        Destroy(gameObject); // Şimdilik oyuncuyu yok edelim
    }
}
