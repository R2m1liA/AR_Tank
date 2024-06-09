using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text timeText;        // 时间显示文本
    public TMP_Text scoreText;       // 分数显示文本
    public GameObject resultPanel; // 结算界面面板
    public Text resultText;      // 结算结果文本

    private float gameTime = 60f; // 游戏时长
    private int score = 0;        // 游戏分数
    private bool gameEnded = false;

    void Start()
    {
        // 初始化时间和分数
        UpdateTimeText();
        UpdateScoreText();
        resultPanel.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        // 更新游戏时间
        gameTime -= Time.deltaTime;
        if (gameTime <= 0)
        {
            gameTime = 0;
            EndGame();
        }

        UpdateTimeText();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateTimeText()
    {
        timeText.text = Mathf.Ceil(gameTime).ToString() + "s";
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    public void PlayerDied()
    {
        EndGame();
    }

    void EndGame()
    {
        gameEnded = true;
        resultText.text = "Game Over\nFinal Score: " + score.ToString();
        resultPanel.SetActive(true);
        // 暂停游戏
        Time.timeScale = 0f;
    }
    /*
    // 可选：添加一个重启游戏的方法
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/
}
