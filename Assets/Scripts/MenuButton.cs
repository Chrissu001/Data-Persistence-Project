using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
   

    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void QuitGame()
    {
#if(UNITY_EDITOR)
     UnityEditor.EditorApplication.isPlaying = false;
#else
     Application.Quit();
#endif

    }

}
