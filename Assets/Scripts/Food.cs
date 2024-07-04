using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;

    private void Start() {
        RandomizePosition();
        RandomizeTexture();
    }

    private void RandomizePosition() {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") || other.CompareTag("Obstacle")) {
            Debug.Log(other.gameObject.name);
            RandomizePosition();
            RandomizeTexture();
        }
    }

    private void RandomizeTexture() {
        Sprite[] foodImages = Resources.LoadAll<Sprite>("Planets");

        if (foodImages.Length == 0) {
            Debug.LogError("No images found in Planets folder!");
            return;
        }

        int randomIndex = Random.Range(0, foodImages.Length);

        Sprite randomSprite = foodImages[randomIndex];

        this.GetComponent<SpriteRenderer>().sprite = randomSprite;
    }
}
