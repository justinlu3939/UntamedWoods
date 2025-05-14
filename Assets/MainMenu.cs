using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("Tutorial");
    }
    public void OpenSettings() {
        //SceneManager.LoadScene("Settings");
    }
    public void QuitGame() {
        Application.Quit();
    }
}
