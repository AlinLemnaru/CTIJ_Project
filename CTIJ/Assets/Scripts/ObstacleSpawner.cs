using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private float[] spawnY;
    [SerializeField] private float spawnXOffset;
    [SerializeField] private float moveSpeed = 6f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            float wait = GameProgressManager.Instance.GetSpawnInterval();     yield return new WaitForSeconds(wait);
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

        GameObject obstacle = Instantiate(obstaclePrefabs[index], spawnPos, Quaternion.identity);

        // attach mover
        ObstacleMover mover = obstacle.AddComponent<ObstacleMover>();
        mover.speed = GameProgressManager.Instance.GetCurrentSpeed();
    }
}
