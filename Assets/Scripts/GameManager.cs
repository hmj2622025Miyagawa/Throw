using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // 画面のテキスト
    [SerializeField] private TextMeshProUGUI timerText; // 画面のテキスト
    [SerializeField] private float timeLimit = 20f; // 制限時間


    private int score = 0; // 今のスコア
    private bool isGameOver = false; // ゲームが終了したかどうか

    // スコアを増やすための関数
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score; // 画面の表示
    }

// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return;

        // 時間を減らす
        timeLimit -= Time.deltaTime;

        // マイナスにならないようにする
        if (timeLimit <= 0)
        {
            timeLimit = 0;
            GameOver();
        }

        // 画面のタイマーを更新
        timerText.text = "Time: " + Mathf.FloorToInt(timeLimit).ToString();
    }

    void GameOver()
    {
        isGameOver = true;
        timerText.text = "TIME UP!";
        Debug.Log("ゲーム終了!最終スコア: " + score);
    }
}
