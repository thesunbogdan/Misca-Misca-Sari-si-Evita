using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverPanel;

    public TextMeshProUGUI scoreText;

    int score = 0;

    public TextMeshProUGUI bestText;

    public TextMeshProUGUI currentText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

    public void GameOver()
    {
        Invoke("ShowOverPanel", 2.0f);
    }

    void ShowOverPanel()
    {
        scoreText.gameObject.SetActive(false);
        if (score > PlayerPrefs.GetInt("Best", 0))
        {
            PlayerPrefs.SetInt("Best", score);
        }
        bestText.text =
            "Best Score: " + PlayerPrefs.GetInt("Best", 0).ToString();
        currentText.text = "Current Score: " + score.ToString();
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void IncrementScoreStar()
    {
        score=score+10;
        scoreText.text = score.ToString();
    }
}
