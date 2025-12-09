using UnityEngine;

public class WeaponAutoAim : MonoBehaviour
{
    [Header("Ayarlar")]
    public Transform firePoint; // Merminin çıkacağı namlu ucu
    public GameObject bulletPrefab; // Mermi prefabı
    public float fireRate = 0.5f; // Saniyede kaç mermi
    public float range = 10f; // Düşman algılama menzili
    public LayerMask enemyLayer; // Sadece düşmanları algılamak için katman

    private float nextFireTime = 0f;
    private Transform target; // Kilitlendiğimiz düşman

    void Update()
    {
        FindClosestEnemy(); // Sürekli en yakın düşmanı ara

        if (target != null)
        {
            // Düşman menzil dışına çıkarsa hedefi bırak
            if(Vector2.Distance(transform.position, target.position) > range)
            {
                target = null;
                return;
            }

            RotateTowardsTarget(); // Düşmana dön

            // Ateş etme zamanı geldiyse ateş et
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void FindClosestEnemy()
    {
        // Belirlenen menzildeki (daire içindeki) tüm objeleri bul
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (Collider2D enemy in enemies)
        {
            // "Enemy" tag'ine sahip mi kontrol et (Layer kullansan da garanti olsun)
            if (enemy.CompareTag("Enemy"))
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy.gameObject;
                }
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void RotateTowardsTarget()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +90f; // -90f sprite'ın yönüne göre değişebilir
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot()
    {
        if(bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    // Editörde menzili görebilmek için gizmo çizimi
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}