﻿using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 5f;

    private Rigidbody2D rb;
    private Gun gun;

    [NonSerialized]
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

    public enum Direction {
        Up,
        Down,
        Left,
        Right
    }

    private void FixedUpdate() {
        rb.velocity = movementVector * speed;

        footsteps.ShouldPlay = rb.velocity.magnitude > .9f * speed;

        movementVector = Vector2.MoveTowards(movementVector, Vector2.zero, Time.deltaTime * 3f);

        SetAnimation();
    }

    private void SetAnimation() {
        var dir = GetFacingDir();
        var prefix = movementVector == Vector2.zero ? "Idle" : "Walk";
        switch (dir) {
            case Direction.Up:
                animationPlayer.EnsurePlaying(prefix + "Up");
                break;
            case Direction.Down:
                animationPlayer.EnsurePlaying(prefix + "Down");
                break;
            case Direction.Left:
                animationPlayer.EnsurePlaying(prefix + "Left");
                break;
            case Direction.Right:
                animationPlayer.EnsurePlaying(prefix + "Right");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public Direction GetFacingDir() {
        Direction dir;
        if (FacingDirection.y < 0) {
            dir = Direction.Down;
        }
        else if (FacingDirection.y > 0) {
            dir = Direction.Up;
        }
        else if (FacingDirection.x > 0) {
            dir = Direction.Right;
        }
        else {
            dir = Direction.Left;
        }

        return dir;
    }

    private void TurnToFaceDirection(Vector2 dir) {
        if (!gun.IsShooting) {
            FacingDirection = dir;
        }
    }

    public void StopVelocity() {
        rb.velocity = Vector2.zero;
        movementVector = Vector2.zero;
    }

    public IEnumerator DoDamageReaction() {
        animationPlayer.LockInto("Damaged");
        yield return new WaitForSeconds(.2f);
        animationPlayer.UnLock("Damaged");

    }
}