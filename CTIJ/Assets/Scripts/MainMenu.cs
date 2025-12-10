using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {

       Application.Quit(); // Quiting the final build

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop playing the game in the editor
#endif

    }
}
