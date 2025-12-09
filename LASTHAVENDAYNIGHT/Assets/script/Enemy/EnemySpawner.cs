using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Düşman prefabını buraya sürükle
    public float spawnRate = 2f; // Kaç saniyede bir düşman çıksın?
    public float spawnDistance = 10f; // Oyuncudan ne kadar uzakta doğsun?

    private Transform player;
    private float nextSpawnTime;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return; // Oyuncu öldüyse spawnlamayı durdur

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        // Rastgele bir yön belirle (Çember şeklinde)
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        
        // Oyuncunun pozisyonuna bu yönü ve mesafeyi ekle
        Vector2 spawnPos = (Vector2)player.position + (randomDirection * spawnDistance);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}