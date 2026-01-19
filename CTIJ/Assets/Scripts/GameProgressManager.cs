using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance;

    [Header("Progression")]
    [SerializeField] private float baseSpeed = 6f;
    [SerializeField] private float speedPerMeter = 0.02f;     // +0.02 speed per meter
    [SerializeField] private float baseSpawnInterval = 2.5f;
    [SerializeField] private float spawnIntervalPerMeter = -0.01f;  // decreases interval
    [SerializeField] private float minSpawnInterval = 0.8f;   // fastest spawn rate
    [SerializeField] private float maxSpeed = 8f;

    private float currentSpeed;
    private float currentSpawnInterval;

    public int Distance { get; private set; }

    void Awake()
    {
        Instance = this;
        currentSpeed = baseSpeed;
        currentSpawnInterval = baseSpawnInterval;
    }

    public float GetCurrentSpeed()
    {
        if(Distance >=550)
            return currentSpeed; // cap speed increase after 600 meters
        if (Distance % 1 == 0 && Distance != 0)
        {
            currentSpeed += speedPerMeter; // small boost every 50 meters
            if (currentSpeed > maxSpeed) 
                currentSpeed = maxSpeed;
        }
        return currentSpeed;
    }

    public float GetSpawnInterval()
    {
        if (Distance >= 550)
            return currentSpawnInterval; // cap spawn rate increase after 600 meters
        if (Distance % 1 == 0 && Distance != 0)
        {
            currentSpawnInterval += spawnIntervalPerMeter; // decrease interval every 30 meters
            if (currentSpawnInterval < minSpawnInterval)
                currentSpawnInterval = minSpawnInterval;

        }
        return currentSpawnInterval;
    }

    public void AddDistance(int meters)
    {
        Distance += meters;
    }
}
