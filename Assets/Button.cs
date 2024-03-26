using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    // Reference to the ZombieMenu script
    public ZombieMenu zombieMenu;

    // Function to be called when the PlayButton is clicked
    public void OnPlayClick()
    {
        // Load scene 1
        SceneManager.LoadScene(1); // Replace "Scene1" with the name of your scene

      
    }

    // Function to be called when the QuitButton is clicked
    public void OnQuitClick()
    {
        // Quit the application
        Application.Quit();
    }
}
