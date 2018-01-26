using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 5f;

    public KeyCode up, down, left, right;

    private Rigidbody2D rb;
    private Vector2 input;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        input = Vector2.zero;
        if (Input.GetKey(up)) {
            input += Vector2.up;
        }

        if (Input.GetKey(left)) {
            input += Vector2.left;
        }

        if (Input.GetKey(down)) {
            input += Vector2.down;
        }

        if (Input.GetKey(right)) {
            input += Vector2.right;
        }

        input = input.normalized * speed;
    }

    private void FixedUpdate() {
        rb.velocity = input;
    }
}