using System.Security;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 5f;
    public Transform gunIndicator;

    private Rigidbody2D rb;
    private Gun gun;

    private Vector2 moveDir;
    public Vector2 FacingDirection { get; private set; } = Vector2.down;
    public Vector2 Position => transform.position;

    private float gunIndicatorZPos;
    private float startingZPos;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        gun = GetComponent<Gun>();
        gunIndicatorZPos = gunIndicator.localPosition.z;
        startingZPos = transform.position.z;
    }

    public void MoveInDir(Vector2 direction) {
        moveDir = direction;
        if (!gun.IsShooting) {
            TurnToFaceDirection(moveDir);
        }
    }

    private void FixedUpdate() {
        rb.velocity = moveDir * speed;

        moveDir = Vector2.MoveTowards(moveDir, Vector2.zero, Time.deltaTime * 3f);
    }

    public void UpdateZPos(float zPos) {
        transform.position = transform.position.WithZ(zPos);
    }

    public void ReturnToDefaultZPos() {
        UpdateZPos(startingZPos);
    }

    private void TurnToFaceDirection(Vector2 dir) {
        if (!gun.IsShooting) {
            FacingDirection = dir;
            gunIndicator.transform.position = (Position + FacingDirection * .7f);
            gunIndicator.transform.localPosition = gunIndicator.transform.localPosition.WithZ(gunIndicatorZPos);

            var angle = Vector2.Angle(Vector2.down, FacingDirection);
            if (FacingDirection.x < 0) {
                angle *= -1;
            }
            
            gunIndicator.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    public void StopVelocity() {
        rb.velocity = Vector2.zero;
        moveDir = Vector2.zero;
    }
}