using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    public Transform segmentPrefab;
    public Vector2Int direction = Vector2Int.right;
    public float speed = 20f;
    public float speedMultiplier = 1f;
    public bool canMove = true;
    public int initialSize = 4;
    public bool moveThroughWalls = false;

    private readonly List<Transform> segments = new List<Transform>();
    private Vector2Int input;
    private float nextUpdate;
    private ScoreManager scoreManager;

    [SerializeField] private Countdown countdownTimer; // Assign your Countdown script here

    private void Start()
    {
        // Initializes the snake and assigns the ScoreManager at the start of the game
        ResetState();
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    private void Update()
    {
        // Handles player input to change the snake's direction
        if (countdownTimer != null && !countdownTimer.CanStart)
            return;

        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                input = Vector2Int.up;
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                input = Vector2Int.down;
        }
        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                input = Vector2Int.right;
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                input = Vector2Int.left;
        }
    }

    private void FixedUpdate()
    {
        // Moves the snake, updates segment positions, and checks for self-collision
        if (countdownTimer != null && !countdownTimer.CanStart)
            return;

        if (Time.time < nextUpdate)
            return;

        if (input != Vector2Int.zero)
            direction = input;

        for (int i = segments.Count - 1; i > 0; i--)
            segments[i].position = segments[i - 1].position;

        int x = Mathf.RoundToInt(transform.position.x) + direction.x;
        int y = Mathf.RoundToInt(transform.position.y) + direction.y;
        transform.position = new Vector2(x, y);

        nextUpdate = Time.time + (1f / (speed * speedMultiplier));

        for (int i = 1; i < segments.Count; i++)
        {
            if (Vector2.Distance(segments[i].position, transform.position) < 0.1f)
            {
                GameManager.Instance.GameOver();
                return;
            }
        }
    }

    public void Grow()
    {
        // Adds a new segment to the snake and increases the score
        Transform segment = Instantiate(segmentPrefab);
        segments.Add(segment);

        segment.position = segments[segments.Count - 2].position;

        if (scoreManager != null)
            scoreManager.AddScore(1);
    }

    public void ResetState()
    {
        // Resets the snake’s position, direction, segments, and score to the initial state
        direction = Vector2Int.right;
        transform.position = Vector3.zero;

        for (int i = 1; i < segments.Count; i++)
            Destroy(segments[i].gameObject);

        segments.Clear();
        segments.Add(transform);

        for (int i = 0; i < initialSize - 1; i++)
            Grow();

        if (scoreManager != null)
            scoreManager.ResetScore();
    }

    public bool Occupies(int x, int y)
    {
        // Checks if the snake currently occupies the given grid coordinates
        foreach (Transform segment in segments)
        {
            if (Mathf.RoundToInt(segment.position.x) == x &&
                Mathf.RoundToInt(segment.position.y) == y)
                return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Handles collisions with food, meat, bombs, obstacles, and triggers game over or score updates
        if (other == null) return;

        if (other.CompareTag("Food"))
        {
            Grow();
        }
        else if (other.CompareTag("Meat"))
        {
            if (scoreManager != null)
                scoreManager.AddScore(scoreManager.GetCurrentScore());

            Meat meat = other.GetComponent<Meat>();
            if (meat != null)
                meat.Relocate();
        }
        else if (other.CompareTag("Bomb"))
        {
            GameManager.Instance.GameOver();

            Bomb bomb = other.GetComponent<Bomb>();
            if (bomb != null)
                bomb.Relocate();
        }
        else if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }

        for (int i = 1; i < segments.Count - 1; i++)
        {
            if (segments[i].position == transform.position)
            {
                GameManager.Instance.GameOver();
                return;
            }
        }
    }

    private void Traverse(Transform wall)
    {
        // Moves the snake to the opposite side when traversing through walls
        Vector3 position = transform.position;

        if (direction.x != 0f)
            position.x = Mathf.RoundToInt(-wall.position.x + direction.x);
        else if (direction.y != 0f)
            position.y = Mathf.RoundToInt(-wall.position.y + direction.y);

        transform.position = position;
    }
}
