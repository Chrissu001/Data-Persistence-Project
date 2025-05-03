using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text playerNameText;

     void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        string highScorePlayer = PlayerPrefs.GetString("HighScoreName","Unknown Player");
        playerNameText.text = $"Best Score: {highScorePlayer} {highScore}";
    }


}

