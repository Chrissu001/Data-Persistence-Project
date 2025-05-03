using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour //This is an example of Inheritance
{
    public InputField nameInput;
   
    public void StartGame()
    {
        PlayerData.playerName = nameInput.text; // Save the name
        SceneManager.LoadScene("main"); // Load the main game scene
    }
}

