using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class LoadScene : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private static string previousScene = "";
    
    // // Make this a singleton that persists between scenes
    // public static LoadScene Instance { get; private set; }
    
    // private void Awake()
    // {
    //     // Simple singleton pattern
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else if (Instance != this)
    //     {
    //         Destroy(gameObject);
    //     }
        
    //     // Register to scene loaded event to ensure time scale is reset
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }
    
    // private void OnDestroy()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }
    
    // // This will be called every time a scene is loaded
    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     // If returning to a gameplay scene (not the start screen)
    //     if (scene.name != "Start Screen")
    //     {
    //         Time.timeScale = 1;
    //         Debug.Log("Setting time scale to 1 in " + scene.name);
    //     }
    // }

    void Update()
    {
        // Check if the player is in range and presses Z
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Load the new scene
            LoadGameScene("Forest (Stage 1)");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object interacting is the player
        if (collision.CompareTag("Player"))
        {
            Debug.Log("in range for the scene");
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Disable interaction when the player leaves the range
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    public void ExitToStart()
    {
        // Store the current scene name before exiting
        previousScene = SceneManager.GetActiveScene().name;
        
        // Pause the game when exiting to start screen
        Time.timeScale = 0;
        
        SceneManager.LoadScene("Start Screen");
    }
    
    // Method to return to the game from the start screen
    public void ReturnToGame()
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            LoadGameScene(previousScene);
        }
        else
        {
            // Default scene if no previous scene is stored
            LoadGameScene("Tutorial");
        }
    }
    
    // Static method that can be called from anywhere
    public static void ReturnToGameStatic()
    {
        // First reset the time scale
        Time.timeScale = 1;
        
        // Then load the scene
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
    
    // Helper method to handle scene loading with proper time scale management
    private void LoadGameScene(string sceneName)
    {
        // Resume normal time scale when loading game scenes
        Time.timeScale = 1;
        Debug.Log("Setting time scale to 1 before loading " + sceneName);
        
        SceneManager.LoadScene(sceneName);
    }
}
