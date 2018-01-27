using UnityEngine;

public class Blinker : MonoBehaviour {

    private PlayerMovement movement;
    private float blinkOffset;
    public float blinkLength;

    public Vector2 Position {
        get { return transform.position; }
        set { transform.position = value; }
    }

    private void Awake() {
        movement = GetComponent<PlayerMovement>();
        blinkOffset = GetComponent<CircleCollider2D>().radius * 1.2f;
    }

    public void TryToBlink() {
        var forward = movement.FacingDirection;
        var destination = Position + blinkLength * forward;

        var toDest = (destination - Position);
        var hit = Physics2D.Raycast(Position, toDest, blinkLength, Layers.GeometryMaskWithoutBlink);
        if (hit) {
            if (hit.distance < blinkOffset)
                return;
            destination = Position + (hit.distance - blinkOffset) * forward;
        }

        Position = destination;
        movement.StopVelocity();
    }
}