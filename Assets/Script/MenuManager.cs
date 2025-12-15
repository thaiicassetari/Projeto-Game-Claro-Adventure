using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBGL
            Application.OpenURL("about:blank");
        #else
            Application.Quit();
        #endif
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Controle()
    {
        SceneManager.LoadScene(3);
    }

    public void Creditos()
    {
        SceneManager.LoadScene(4);
    }
}