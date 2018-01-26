using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 5f;
    public KeyCode up, down, left, right;
    public Transform directionIndicator;

    private Rigidbody2D rb;
    private Gun gun;

    private Vector2 input;
    public Vector2 FacingDirection { get; private set; } = Vector2.down;

    private Vector2 Position => transform.position;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        gun = GetComponent<Gun>();
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
        input = input.normalized;
    }

    private void FixedUpdate() {
        TurnToFaceDirection();

        rb.velocity = input * speed;
    }

    private void TurnToFaceDirection() {
        if (input != Vector2.zero && !gun.IsShooting) {
            FacingDirection = input;
            directionIndicator.transform.position = Position + FacingDirection * .7f;

            var angle = Vector2.Angle(Vector2.down, FacingDirection);
            if (FacingDirection.x < 0) {
                angle *= -1;
            }
            
            directionIndicator.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}