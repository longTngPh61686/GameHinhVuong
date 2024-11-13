using UnityEngine;
using UnityEngine.Tilemaps;

public class BossCircle : MonoBehaviour
{
    public float moveSpeed = 3f;          // Tốc độ di chuyển
    public float detectionRange = 5f;     // Khoảng cách phát hiện Square để đuổi theo
    public Transform target;              // Mục tiêu (Square)
    public Tilemap tilemap;               // TileMap chứa các ô
    private Vector3 nextPosition;

    private void Start()
    {
        ChooseNextPosition();
    }

    private void Update()
    {
        // Kiểm tra khoảng cách giữa BossCircle và Square
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= detectionRange)
        {
            // Nếu Square trong khoảng phát hiện, đuổi theo Square
            MoveTowardsTarget();
        }
        else
        {
            // Nếu không, di chuyển ngẫu nhiên
            MoveRandomly();
        }
    }

    void MoveTowardsTarget()
    {
        // Tính toán hướng di chuyển về phía Square
        Vector3 direction = (target.position - transform.position).normalized;
        nextPosition = transform.position + direction * moveSpeed * Time.deltaTime;

        // Di chuyển BossCircle về phía Square
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
    }

    void MoveRandomly()
    {
        // Di chuyển BossCircle đến vị trí tiếp theo
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

        // Khi đạt đến vị trí tiếp theo, chọn một vị trí mới
        if (Vector3.Distance(transform.position, nextPosition) < 0.1f)
        {
            ChooseNextPosition();
        }
    }

    void ChooseNextPosition()
    {
        Vector3Int currentCell = tilemap.WorldToCell(transform.position);
        Vector3Int[] directions = { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right }; // Di chuyển tự do trên x và y
        Vector3Int chosenCell;

        do
        {
            // Chọn ngẫu nhiên một hướng để di chuyển
            Vector3Int randomDirection = directions[Random.Range(0, directions.Length)];
            chosenCell = currentCell + randomDirection;

            // Chuyển đổi ô được chọn thành vị trí thế giới
            Vector3 potentialPosition = tilemap.GetCellCenterWorld(chosenCell);

            // Kiểm tra va chạm với các ô có tag "NonTouch"
            Collider2D collider = Physics2D.OverlapCircle(potentialPosition, 0.1f);
            if (collider == null || collider.CompareTag("NonTouch") == false)
            {
                nextPosition = potentialPosition;
                break;
            }

        } while (true); // Lặp lại cho đến khi tìm được ô hợp lệ
    }
}










