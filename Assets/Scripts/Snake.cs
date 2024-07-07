using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snake : MonoBehaviour, IScorable
{
    public  GameObject UIManager;
    public Transform segmentPrefab;

    private static Vector2 defaultDirection = Vector2.right;
    private Vector2 direction = defaultDirection;
    private Queue<Vector2> queuedDirections = new Queue<Vector2>();
    private List<Transform> segments;

    private void Start() {
        segments = new List<Transform>();
        this.ResetState();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down) { SetDirection(Vector2.up); }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up) { SetDirection(Vector2.down); }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left) { SetDirection(Vector2.right); }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right) { SetDirection(Vector2.left); }
    }

    private void FixedUpdate() {
        this.MoveSnake();
    }

    private void MoveSnake() {
        Vector2 nextDirection = this.GetNextDirection();

        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + nextDirection.x,
            Mathf.Round(this.transform.position.y) + nextDirection.y,
            0.0f
            );
    }

    private void Grow() {
        Transform newSegment = Instantiate(this.segmentPrefab);
        newSegment.position = segments[this.segments.Count - 1].position;

        segments.Add(newSegment);
    }

    public void ResetState() {

        // Remove Segments
        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(this.transform);

        // Reset position
        this.transform.position = Vector3.zero;

        // Reset Direction
        direction = defaultDirection;
    }

    private void SetDirection(Vector3 direction) {
        queuedDirections.Enqueue(direction);
        this.direction = direction;
    }

    private Vector3 GetNextDirection() {
        if (queuedDirections.Count > 0) {
            return queuedDirections.Dequeue();
        } else {
            return this.direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Food")) {
            Grow();
        }
        else if (other.CompareTag("Obstacle")) {
            UIManager.GetComponent<UIManager>().EndGame();
        }
    }

    public int GetScore() {
        return this.segments.Count;
    }
}
