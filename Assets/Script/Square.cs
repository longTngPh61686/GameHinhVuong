using UnityEngine;
using UnityEngine.SceneManagement;

public class Square : MonoBehaviour
{
    public float speed = 5f;
    public GameObject winPanel;  // Panel hiện khi chiến thắng
    public GameObject bossCirclePrefab; // Prefab BossCircle
    public Transform spawnPoint; // Điểm spawn BossCircle

    private Vector3 startPoint;  // Lưu điểm xuất phát
    private Rigidbody2D rb;

    void Start()
    {
        // Lưu lại vị trí xuất phát
        startPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();

        // Đảm bảo panel chiến thắng bị ẩn khi bắt đầu
        if (winPanel != null)
            winPanel.SetActive(false);

        // Gọi SpawnBossCircle sau 5 giây
        Invoke("SpawnBossCircle", 5f);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, vertical, 0).normalized;
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Circle"))
        {
            // Đưa hình vuông về vị trí xuất phát
            transform.position = startPoint;
        }
        if (collision.gameObject.CompareTag("NonTouch"))
        {
            // Đưa hình vuông về vị trí xuất phát
            transform.position = startPoint;
            Debug.Log("Va chạm với nonTouch. Về điểm xuất phát.");
        }
        if (collision.gameObject.CompareTag("BossCircle"))
        {
            // Đưa hình vuông về vị trí xuất phát
            transform.position = startPoint;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Khi chạm vào WinZone, hiển thị panel chiến thắng
        if (other.CompareTag("WinZone"))
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // Phương thức chơi lại khi nhấn nút Play Again
    public void PlayAgain()
    {
        // Ẩn Panel Win
        winPanel.SetActive(false);
        Time.timeScale = 1f;

        // Tải lại scene hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Đưa Square về điểm xuất phát sau khi scene được tải lại
        transform.position = startPoint;
    }

    // Phương thức spawn BossCircle
    void SpawnBossCircle()
    {
        if (bossCirclePrefab != null && spawnPoint != null)
        {
            Instantiate(bossCirclePrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}



