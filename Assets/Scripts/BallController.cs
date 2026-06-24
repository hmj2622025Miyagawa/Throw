using UnityEditor.Rendering;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    private GameManager gameManager;

    public AudioClip hitSound;
    public AudioClip flySound;

    private AudioSource audioSource;
    private bool isGameOver = false;

    // 💡【変更】不具合の原因になっていたタイマー変数はすべて削除しました

    void Awake()
    {
        Application.targetFrameRate = 60;
        gameManager = Object.FindFirstObjectByType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("画面内に GameManager オブジェクトが見つかりません！ヒエラルキーを確認してください。");
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir);
    }

    // Update内のタイマー処理が不要になったため、空にしています
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = false;

        // 💡飛行音（ループ音）が鳴っている最初の1回だけ音を止めます
        if (audioSource != null && audioSource.loop)
        {
            audioSource.Stop();
            audioSource.loop = false;
        }

        string targetTag = collision.gameObject.tag;

        // 床なら何もしない
        if (targetTag == "Floor")
        {
            return;
        }

        // タグによって点数変える
        int scoreValue = 0;
        if (targetTag == "Bin") { scoreValue = 5; }
        else if (targetTag == "Cube") { scoreValue = 1; }
        else if (targetTag == "TrashBox") { scoreValue = 10; }
        else if (targetTag == "GarbageBag") { scoreValue = 15; }
        else if (targetTag == "Spray") { scoreValue = 10; }
        else if (targetTag == "Bottle") { scoreValue = 10; }
        else if (targetTag == "Block") { scoreValue = 10; }
        else if (targetTag == "Gasoline") { scoreValue = -40; }
        else { scoreValue = 1; }

        // スコアを加算
        if (gameManager != null)
        {
            gameManager.AddScore(scoreValue);
        }

        // エフェクト生成とオブジェクト削除
        Instantiate(effectPrefab, collision.transform.position, collision.transform.rotation);
        Destroy(collision.gameObject);

        if (isGameOver) return;

        if (audioSource != null && audioSource.isActiveAndEnabled && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    public void PlayThrowSound()
    {
        if (isGameOver) return;

        if (audioSource != null && flySound != null)
        {
            audioSource.clip = flySound;
            audioSource.loop = true; // 投げた時はループをONにする
            audioSource.Play();
        }
    }

    public void SetGameOver()
    {
        isGameOver = true;
    }
}
