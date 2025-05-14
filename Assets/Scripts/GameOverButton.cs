using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    public void LoadStartScreen() {
        Debug.Log("Button Clicked!");
        SceneManager.LoadScene("Start Screen");
    }
    public void LoadBossFight() {
        SceneManager.LoadScene("Forest (Stage 1)");
    }
}
