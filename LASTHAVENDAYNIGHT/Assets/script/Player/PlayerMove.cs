using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Ayarlar")]
    public float moveSpeed = 5f; // Hareket hızı
    public Rigidbody2D rb;       // Karakterin Rigidbody bileşeni
    public Animator animator;    // Karakterin Animator bileşeni

    private Vector2 movement;    // X ve Y eksenindeki hareket girdisi

    // Her karede çalışır (Input işlemleri burada yapılır)
    void Update()
    {
        // 1. Girdileri Al
        // Horizontal: A/D veya Sol/Sağ Ok, Vertical: W/S veya Yukarı/Aşağı Ok
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // 2. Animasyon Parametrelerini Ayarla
        // 'Speed' parametresi karakterin hareket edip etmediğini kontrol eder
        // sqrMagnitude, hızın karesini alır (performans için magnitude yerine kullanılır)
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // 3. Karakterin Yönünü Çevir (Flipping)
        if (movement.x > 0||movement.y > 0)
        {
            // Sağa giderken normal duruş
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
        else if (movement.x < 0 || movement.y < 0)
        {
            // Sola giderken ters çevir
            transform.localScale = new Vector3(-0.4f, 0.4f, 0.4f);
        }
    }

    // Sabit zaman aralıklarında çalışır (Fizik işlemleri burada yapılır)
    void FixedUpdate()
    {
        // Karakteri hareket ettir
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
