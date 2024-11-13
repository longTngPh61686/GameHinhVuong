using UnityEngine;

public class Boss : MonoBehaviour
{
    public float moveSpeed = 3f;           // Tốc độ di chuyển của Boss
    public Transform player;               // Tham chiếu đến đối tượng người chơi
    private Vector3 targetPosition;        // Vị trí mục tiêu để Boss di chuyển đến
    private float delayTime = 5f;          // Độ trễ trước khi Boss bắt đầu di chuyển
    private float timer = 0f;              // Biến để theo dõi thời gian đã trôi qua

    private void Update()
    {
        if (player != null)
        {
            // Tăng timer mỗi frame
            timer += Time.deltaTime;

            if (timer >= delayTime)
            {
                // Nếu thời gian đã vượt qua độ trễ, bắt đầu di chuyển về người chơi
                MoveTowardsPlayer();
            }
        }
    }

    // Hàm di chuyển Boss về hướng người chơi
    void MoveTowardsPlayer()
    {
        // Tính toán hướng di chuyển từ Boss đến người chơi
        Vector3 direction = (player.position - transform.position).normalized;

        // Cập nhật vị trí mục tiêu
        targetPosition = transform.position + direction * moveSpeed * Time.deltaTime;

        // Di chuyển Boss về vị trí mục tiêu
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}

