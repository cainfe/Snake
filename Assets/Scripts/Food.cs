using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;

    private void Start() {
        RandomizePosition();
    }

    private void RandomizePosition() {
        Bounds bounds = this.gridArea.bounds;
        bool positionEmpty;

        do {
            positionEmpty = true;
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);

            this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);

            Collider2D colliders = Physics2D.OverlapBox(this.transform.position, transform.localScale / 2, 0f, 0);

            if (colliders != null &&
                (colliders.CompareTag("Obstacle") ||
                 colliders.CompareTag("Player"))
                ) {
                positionEmpty = false;
            }
        } while (!positionEmpty);

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
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
