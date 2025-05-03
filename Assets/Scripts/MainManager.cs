using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;
    private int remainingBricks; // Track bricks count

    void Start()
    {
        GenerateBricks(); // Initialize bricks at the start
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int points)
    {
        m_Points += points;
        ScoreText.text = $"{PlayerData.playerName} Score: {m_Points}";
    }

    void BrickDestroyed(int points) // Track score when brick is destroyed
    {
        m_Points += points; // Add points to score
        remainingBricks--;

        ScoreText.text = $"{PlayerData.playerName} Score: {m_Points}"; // Update score display

        if (remainingBricks <= 0)
        {
            Invoke(nameof(GenerateBricks), 1f); // Delay before regenerating bricks
        }
    }

    void GenerateBricks()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };

        remainingBricks = LineCount * perLine; // Reset brick counter

        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(BrickDestroyed); // Track destroyed bricks & score
            }
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        string highScorePlayer = PlayerPrefs.GetString("HighScoreName", "Unknown Player");

        if (m_Points > highScore)
        {
            PlayerPrefs.SetInt("HighScore", m_Points);
            PlayerPrefs.SetString("HighScoreName", PlayerData.playerName);
            PlayerPrefs.Save();
        }
    }
}
