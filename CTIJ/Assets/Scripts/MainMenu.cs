using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void onEndlessButtonPressed()
    {
        SceneManager.LoadScene("EndlessMode");
    }

    public void onSettingsButtonPressed()
    {
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame()
    {

       Application.Quit(); // Quiting the final build

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop playing the game in the editor
#endif

    }
}
