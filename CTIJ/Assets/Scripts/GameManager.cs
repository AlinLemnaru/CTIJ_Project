using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Death Menu")]
    [SerializeField] private GameObject deathMenuCanvas;
    [SerializeField] private float deathMenuDelay;

    [Header("Gameplay References")]
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private LoopParallax[] parallaxLayers;
    [SerializeField] private PlayerController playerController;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuCanvas;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle pause menu
            if (pauseMenuCanvas.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
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

        // 5. Start coroutine to wait for death anim, then freeze + menu
        StartCoroutine(HandleDeathAfterAnimation());

        // 5. Show death menu UI
        // if (deathMenuCanvas != null)
        //   deathMenuCanvas.SetActive(true);

        // 6. Pause time for everything that uses Time.deltaTime
        // Time.timeScale = 0f;
    }

    private IEnumerator HandleDeathAfterAnimation()
    {
        // Wait so the death animation can fully play
        yield return new WaitForSeconds(deathMenuDelay);

        // Show death menu UI
        if (deathMenuCanvas != null)
            deathMenuCanvas.SetActive(true);

        // Freeze game
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

    public void PauseGame(bool status)
    {
        // If pausing, set time scale to 0 else set to 1
        if (status)
        {
            if (deathMenuCanvas.activeInHierarchy) return;  // Do not pause if death menu is active

            Time.timeScale = 0f;

            if (playerController != null)
                playerController.enabled = false;

            if (SoundManager.instance != null)
            {
                SoundManager.instance.StopRunLoop();
                SoundManager.instance.StopSlideLoop();
            }
        }
        else
        {
            Time.timeScale = 1f;

            if (playerController != null)
                playerController.enabled = true;
        }

        // If status is true, pause the game
        // If status is false, unpause the game
        pauseMenuCanvas.SetActive(status);
    }
}
