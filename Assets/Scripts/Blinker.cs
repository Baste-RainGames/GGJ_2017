using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Blinker : MonoBehaviour {

    public float blinkLength;
    public AudioSource audioSource;
    public float blinkCooldown;

    private PlayerMovement movement;
    private float blinkOffset;
    private float canBlinkTime;
    private bool blinking;

    public Vector2 Position {
        get { return transform.position; }
        set { transform.position = value; }
    }

    private void Awake() {
        movement = GetComponent<PlayerMovement>();
        blinkOffset = GetComponent<CircleCollider2D>().radius * 1.2f;
    }

    public void TryToBlink() {
        if(Time.time < canBlinkTime || blinking)
            return;

        var forward = movement.FacingDirection;
        var dir = movement.GetFacingDir(); //well this is the same information in two diffent ways!
        var destination = Position + blinkLength * forward;
        audioSource.Play();

        var toDest = (destination - Position);
        var hit = Physics2D.Raycast(Position, toDest, blinkLength, Layers.GeometryMaskWithoutBlink);
        if (hit) {
            if (hit.distance < blinkOffset)
                return;
            destination = Position + (hit.distance - blinkOffset) * forward;
        }

        StartCoroutine(DoBlink(destination, dir));
    }

    private IEnumerator DoBlink(Vector2 destination, PlayerMovement.Direction dir) {
        blinking = true;
        SetBlinkAnim(dir);
        movement.StopVelocity();
        movement.enabled = false;
        yield return new WaitForSeconds(.2f);
        Position = destination;
        yield return new WaitForSeconds(.1f);
        movement.enabled = true;
        blinking = false;
        canBlinkTime = Time.time + blinkCooldown;
    }

    private void SetBlinkAnim(PlayerMovement.Direction dir) {
        switch (dir) {
            case PlayerMovement.Direction.Up:
                movement.animationPlayer.Play("TeleportUp");
                break;
            case PlayerMovement.Direction.Down:
                movement.animationPlayer.Play("TeleportDown");
                break;
            case PlayerMovement.Direction.Left:
                movement.animationPlayer.Play("TeleportLeft");
                break;
            case PlayerMovement.Direction.Right:
                movement.animationPlayer.Play("TeleportRight");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
        }
    }
}