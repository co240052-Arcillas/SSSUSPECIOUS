using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Food : MonoBehaviour
{
    public Collider2D gridArea;
    public LayerMask obstacleLayer;

    private Snake snake;
    private Collider2D col;

    private void Awake()
    {
        // Initializes references to the Snake and Collider2D components
        snake = FindFirstObjectByType<Snake>();
        col = GetComponent<Collider2D>();
        if (col == null)
            Debug.LogWarning("Food has no Collider2D! Defaulting to 1x1 size.");
    }

    private void Start()
    {
        // Sets the food to a random position at the start
        RandomizePosition();
    }

    private bool IsBlocked(Vector2 position)
    {
        // Checks if the given position is blocked by an obstacle
        Vector2 size = col != null ? col.bounds.size : Vector2.one;
        return Physics2D.OverlapBox(position, size, 0f, obstacleLayer);
    }

    public void RandomizePosition()
    {
        // Finds a random free position within the grid area and places the food there
        Bounds bounds = gridArea.bounds;
        Vector2 newPos;
        int attempts = 0;
        const int maxAttempts = 100;

        Vector2 halfSize = col != null ? col.bounds.extents : new Vector2(0.5f, 0.5f);

        float spawnMinX = bounds.min.x + halfSize.x;
        float spawnMaxX = bounds.max.x - halfSize.x;
        float spawnMinY = bounds.min.y + halfSize.y;
        float spawnMaxY = bounds.max.y - halfSize.y;

        do
        {
            float x = Random.Range(spawnMinX, spawnMaxX);
            float y = Random.Range(spawnMinY, spawnMaxY);
            newPos = new Vector2(Mathf.Round(x), Mathf.Round(y));

            attempts++;
            if (attempts > maxAttempts)
            {
                Debug.LogWarning("Could not find free spot for Food after 100 attempts!");
                break;
            }

        } while ((snake != null && snake.Occupies((int)newPos.x, (int)newPos.y)) || IsBlocked(newPos));

        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Moves the food to a new random position when the snake touches it
        if (other.CompareTag("Player") || other.CompareTag("SnakeHead"))
            RandomizePosition();
    }
}
