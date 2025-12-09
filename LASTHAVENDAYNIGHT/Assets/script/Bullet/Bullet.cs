using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 20f;
    public float lifeTime = 2f; // Mermi boşa giderse 2 saniye sonra yok olsun

    void Start()
    {
        // Mermiyi oluşturulduğu anda ileri fırlat (Rigidbody2D kullanıyorsan velocity daha iyidir)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed; 
        
        // Sonsuza kadar sahnede kalmasın
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Çarptığımız objede EnemyController var mı?
        EnemyController enemy = hitInfo.GetComponent<EnemyController>();
        
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject); // Mermiyi yok et
        }
        // Eğer duvara çarparsa da yok olsun (Tag kontrolü eklenebilir)
        else if (!hitInfo.CompareTag("Player")) 
        {
            Destroy(gameObject);
        }
    }
}
