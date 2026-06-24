using UnityEngine;
using UnityEngine.InputSystem;

public class BallGenerator : MonoBehaviour
{
    public  GameObject ballPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Mouse.current.leftButton.wasPressedThisFrame)
        //{
        //    GameObject ball = Instantiate(ballPrefab, transform.position, transform.rotation);
        //    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);
        //    ball.GetComponent<BallController>().Shoot(ray.direction * 1000);
        //}

        //BallController ballControllerScript = ball.GetCompornent<BallController>();
        //if (ballControllerScript != null)
        //{
        //    ballControllerScript.PlayThrowSound();
        //}


        // マウスの左クリックを検知
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // 1. ボールを生成
            GameObject ball = Instantiate(ballPrefab, transform.position, transform.rotation);

            // 2. 飛ばす処理
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);
            ball.GetComponent<BallController>().Shoot(ray.direction * 1000);

            // 💡 3. 【修正】生成した直後（ifの中）でスクリプトを取得して音を鳴らす
            BallController ballControllerScript = ball.GetComponent<BallController>();
            if (ballControllerScript != null)
            {
                ballControllerScript.PlayThrowSound();
            }
        }

    }
}
