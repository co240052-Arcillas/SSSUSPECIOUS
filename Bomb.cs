using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public Collider2D gridArea;
    public LayerMask obstacleLayer;
    [SerializeField] private float lifeTime = 6f;

    private Snake snake;
    private Collider2D col;

    private void Awake()
    {
        // Finds the Snake object and gets the Bomb's collider, with a warning if missing
        snake = FindFirstObjectByType<Snake>();
        col = GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogWarning("Bomb has no Collider2D! Defaulting to 1x1 size.");
        }
    }

    private void OnEnable()
    {
        // Starts the coroutine to periodically relocate the bomb
        StartCoroutine(RelocateRoutine());
    }

    private bool IsBlocked(Vector2 position)
    {
        // Checks if the given position is blocked by obstacles
        Vector2 size = col != null ? col.bounds.size : Vector2.one;
        return Physics2D.OverlapBox(position, size, 0f, obstacleLayer);
    }

    public void Relocate()
    {
        // Moves the bomb to a random unoccupied, non-blocked position within the grid
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
                Debug.LogWarning("Could not find free spot for Bomb after 100 attempts!");
                break;
            }

        } while (snake != null && snake.Occupies((int)newPos.x, (int)newPos.y) || IsBlocked(newPos));

        transform.position = newPos;
    }

    private IEnumerator RelocateRoutine()
    {
        // Continuously relocates the bomb every 'lifeTime' seconds
        while (true)
        {
            Relocate();
            yield return new WaitForSeconds(lifeTime);
        }
    }
}
