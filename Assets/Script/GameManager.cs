using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text timeText;        // ʱ����ʾ�ı�
    public TMP_Text scoreText;       // ������ʾ�ı�
    public GameObject resultPanel; // ����������
    public TMP_Text resultText;      // �������ı�

    private float gameTime = 60f; // ��Ϸʱ��
    private int score = 0;        // ��Ϸ����
    private bool gameEnded = false;

    void Start()
    {
        // ��ʼ��ʱ��ͷ���
        UpdateTimeText();
        UpdateScoreText();
        resultPanel.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        // ������Ϸʱ��
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
        // resultText.text = "Game Over\nFinal Score: " + score.ToString();
        resultPanel.SetActive(true);
        // ��ͣ��Ϸ
        Time.timeScale = 0f;
    }
    /*
    // ��ѡ�����һ��������Ϸ�ķ���
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/
}
