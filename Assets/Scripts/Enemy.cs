using System;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Vector2 Position => transform.position;

    public float speed;

    private Rigidbody2D rb;
    private PlayerMovement player1, player2;

    private Vector2 moveDir;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start() {
        try {
            var players = FindObjectsOfType<PlayerMovement>();
            player1 = players[0];
            player2 = players[1];
        }
        catch {
            Debug.LogError("LOL NO PLAYERS");
            Destroy(gameObject);
        }
    }

    void Update() {
        var closest = FindClosestPlayer();
        moveDir = (closest.Position - Position).normalized;
    }

    private void FixedUpdate() {
        rb.velocity = moveDir * speed;
    }

    private PlayerMovement FindClosestPlayer() {
        var dist1 = Vector2.Distance(Position, player1.Position);
        var dist2 = Vector2.Distance(Position, player2.Position);

        return dist1 < dist2 ? player1 : player2;
    }
}