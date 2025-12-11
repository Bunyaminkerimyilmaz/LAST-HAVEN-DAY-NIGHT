using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek obje (Karakterin)
    public float smoothSpeed = 0.125f; // Takip etme yumuşaklığı (0 ile 1 arası)
    public Vector3 offset; // Kamera ile karakter arasındaki mesafe farkı

    // LateUpdate, tüm hareketler bittikten sonra çalışır. 
    // Böylece karakter hareket ederken kamera titreme yapmaz.
    void LateUpdate()
    {
        if (target == null) return; // Eğer karakter öldüyse hata vermesin

        // Gitmek istediğimiz pozisyon (Karakterin yeri + mesafe farkı)
        Vector3 desiredPosition = target.position + offset;
        
        // Lerp fonksiyonu, mevcut konumdan yeni konuma yumuşak geçiş sağlar
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        transform.position = smoothedPosition;
    }
}