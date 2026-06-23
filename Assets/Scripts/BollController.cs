using UnityEditor.Rendering;
using UnityEngine;

public class BollController : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        // Shoot(new Vector3(0, 200, 2000));
        gameManager = Object.FindFirstObjectByType<GameManager>();

        // 万一見つからないときの警告
        if (gameManager == null)
        {
            Debug.LogError("画面内に GameManager オブジェクトが見つかりません！ヒエラルキーを確認してください。");
        }
    }

    public void Shoot(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce (dir);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = false;

        string targetTag = collision.gameObject.tag;

        // 床なら何もしない
        if (targetTag == "Floor") return;
 
        // タグによって点数変える
        int scoreValue = 0;
        if (targetTag == "Bin")
        {
            scoreValue = 5;
        }
        else if (targetTag == "Cube")
        {
            scoreValue = 1;
        }
        else if (targetTag == "TrashBox")
        {
            scoreValue = 10;
        }
        else if (targetTag == "GarbageBag")
        {
            scoreValue = 15;
        }
        else if (targetTag == "Spray")
        {
            scoreValue = 10;
        }
        else if (targetTag == "Bottle")
        {
            scoreValue = 10;
        }
        else if (targetTag == "Block")
        {
            scoreValue = 10;
        }
        else if (targetTag == "Gasoline")
        {
            scoreValue = -40;
        }
        else
        {
            scoreValue = 1;
        }

        // スコアを加算
        if (gameManager != null)
        {
            gameManager.AddScore(scoreValue);
        }
            // エフェクト生成とオブジェクト削除
            Instantiate(effectPrefab, collision.transform.position, collision.transform.rotation);
        Destroy(collision.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
