using UnityEngine;
using UnityEngine.Tilemaps;

public class Circle : MonoBehaviour
{
    public float moveSpeed = 3f;          // Tốc độ di chuyển
    public Tilemap tilemap;               // TileMap chứa các ô
    private Vector3 nextPosition;

    private void Start()
    {
        ChooseNextPosition();
    }

    private void Update()
    {
        // Di chuyển hình tròn đến vị trí tiếp theo
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
        Vector3Int[] directions = { Vector3Int.up, Vector3Int.down }; // Chỉ di chuyển theo trục y
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
