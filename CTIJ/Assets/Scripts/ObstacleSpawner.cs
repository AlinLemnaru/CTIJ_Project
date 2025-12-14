using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private float spawnIntervalMin = 1.5f;
    [SerializeField] private float spawnIntervalMax = 3.0f;
    [SerializeField] private float[] spawnY;
    [SerializeField] private float spawnXOffset;
    [SerializeField] private float moveSpeed = 6f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnLoop());
    }

    System.Collections.IEnumerator SpawnLoop()
    {
        while (true)
        {
            float wait = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(wait);
            int index = Random.Range(0, obstaclePrefabs.Length);
            SpawnObstacle(index);
        }
    }

    void SpawnObstacle(int index)
    {
        if (player == null) return;

        Vector3 spawnPos = new Vector3(
            player.position.x + spawnXOffset,
            spawnY[index],
            0f
        );

        GameObject saw = Instantiate(obstaclePrefabs[index], spawnPos, Quaternion.identity);

        // attach mover
        ObstacleMover mover = saw.AddComponent<ObstacleMover>();
        mover.speed = moveSpeed;
    }
}
