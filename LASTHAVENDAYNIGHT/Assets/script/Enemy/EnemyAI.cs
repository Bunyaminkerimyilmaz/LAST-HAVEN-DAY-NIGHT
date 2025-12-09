using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int damageToGive = 10;
    
    private Transform playerTarget;
    private Rigidbody2D rb;

    void Start()
    {
        // Sahnedeki "Player" etiketli objeyi otomatik bul
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        
        if (playerObj != null)
        {
            playerTarget = playerObj.transform;
        }

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Eğer oyuncu varsa ona doğru yürü
        if (playerTarget != null)
        {
            // Oyuncuya doğru bir yön vektörü oluştur
            Vector2 direction = (playerTarget.position - transform.position).normalized;
            
            // Fizik motorunu kullanarak hareket ettir (Duvarların içinden geçmemesi için)
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            
            // İstersen düşman da yüzünü oyuncuya dönsün:
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle; // Eğer yan dönüyorsa buraya +90 veya -90 ekle (önceki sorundaki gibi)
        }
    }

    // Çarpışma kontrolü
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Çarptığımız şey oyuncu mu?
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToGive);
                // İstersen düşman oyuncuya çarpınca kendini yok edebilir (Kamikaze gibi)
                // Destroy(gameObject); 
            }
        }
    }
}
