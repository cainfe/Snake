using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private static Vector2 defaultDirection = Vector2.right;
    private Vector2 direction = defaultDirection;
    private List<Transform> segments;
    public Transform segmentPrefab;

    private void Start() {
        segments = new List<Transform>();
        segments.Add(this.transform);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down) { direction = Vector2.up; }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up) { direction = Vector2.down; }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left) { direction = Vector2.right; }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right) { direction = Vector2.left; }
    }

    private void FixedUpdate() {
        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
            );
    }

    private void Grow() {
        Transform newSegment = Instantiate(this.segmentPrefab);
        newSegment.position = segments[this.segments.Count - 1].position;

        segments.Add(newSegment);
    }

    private void ResetState() {

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

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Food")) {
            Grow();
        }
        else if (other.CompareTag("Obstacle")) {
            ResetState();
        }
    }
}
