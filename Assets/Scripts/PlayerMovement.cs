using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 5f;
    public Transform gunIndicator;

    private Rigidbody2D rb;
    private Gun gun;

    public Vector2 movementVector;
    public AnimationPlayer animationPlayer;
    private FootstepSounds footsteps;
    public Vector2 FacingDirection { get; private set; } = Vector2.down;
    public Vector2 Position => transform.position;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        gun = GetComponent<Gun>();
        footsteps = GetComponentInChildren<FootstepSounds>();
        animationPlayer = new AnimationPlayer(GetComponentInChildren<Animator>());
        animationPlayer.EnsurePlaying("IdleDown");
    }

    public void MoveInDir(Vector2 direction) {
        movementVector = direction;
        if (!gun.IsShooting) {
            TurnToFaceDirection(movementVector);
        }
    }

    private void FixedUpdate() {
        rb.velocity = movementVector * speed;

        footsteps.ShouldPlay = rb.velocity.magnitude > .9f * speed;

        movementVector = Vector2.MoveTowards(movementVector, Vector2.zero, Time.deltaTime * 3f);
        if (movementVector == Vector2.zero) {
            if (FacingDirection.y < 0) {
                animationPlayer.EnsurePlaying("IdleDown");
            }
            else if (FacingDirection.y > 0) {
                animationPlayer.EnsurePlaying("IdleUp");
            }
            else if (FacingDirection.x > 0) {
                animationPlayer.EnsurePlaying("IdleRight");
            }
            else {
                animationPlayer.EnsurePlaying("IdleLeft");
            }
        }
        else {
            if (FacingDirection.y < 0) {
                animationPlayer.EnsurePlaying("WalkDown");
            }
            else if (FacingDirection.y > 0) {
                animationPlayer.EnsurePlaying("WalkUp");
            }
            else if (FacingDirection.x > 0) {
                animationPlayer.EnsurePlaying("WalkRight");
            }
            else {
                animationPlayer.EnsurePlaying("WalkLeft");
            }
        }
    }


    private void TurnToFaceDirection(Vector2 dir) {
        if (!gun.IsShooting) {
            FacingDirection = dir;
            var zPos = transform.position.z;
            gunIndicator.transform.position = (Position + FacingDirection * .7f).WithZ(zPos - .5f);

            var angle = Vector2.Angle(Vector2.down, FacingDirection);
            if (FacingDirection.x < 0) {
                angle *= -1;
            }
            
            gunIndicator.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    public void StopVelocity() {
        rb.velocity = Vector2.zero;
        movementVector = Vector2.zero;
    }
}