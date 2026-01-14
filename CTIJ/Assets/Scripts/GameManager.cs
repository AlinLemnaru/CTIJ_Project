using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Death Menu")]
    [SerializeField] private GameObject deathMenuCanvas;

    [Header("Gameplay References")]
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private LoopParallax[] parallaxLayers;
    [SerializeField] private PlayerController playerController;

    private bool gameOver;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnPlayerDeath()
    {
        if (gameOver) return;
        gameOver = true;

        // 1. Stop player controls & movement
        if (playerController != null)
        {
            playerController.enabled = false;
            Rigidbody2D rb = playerController.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = Vector2.zero;
        }

        // 2. Stop obstacle spawning
        if (obstacleSpawner != null)
            obstacleSpawner.enabled = false;

        // 3. Stop all parallax layers
        if (parallaxLayers != null)
        {
            foreach (var layer in parallaxLayers)
            {
                if (layer != null)
                    layer.enabled = false;
            }
        }

        // 4. Optionally stop run/slide loops if not already
        if (SoundManager.instance != null)
        {
            SoundManager.instance.StopRunLoop();
            SoundManager.instance.StopSlideLoop();
        }

        // 5. Show death menu UI
        if (deathMenuCanvas != null)
            deathMenuCanvas.SetActive(true);

        // 6. Pause time for everything that uses Time.deltaTime
        Time.timeScale = 0f;
    }

    public void RestartEndless()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("EndlessMode");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
