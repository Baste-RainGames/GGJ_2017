using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour {

    public Vector2 Position => transform.position;

    public float speed = 3f;
    public float aggroDistance = 5f;

    private Rigidbody2D rb;
    private PlayerMovement player1, player2;
    private AnimationPlayer animationPlayer;
    public AudioSource stepSound;

    private Vector2 moveDir;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        var animator = GetComponentInChildren<Animator>();
        animationPlayer = new AnimationPlayer(animator);
        stepSound = GetComponentInChildren<AudioSource>();
    }

    void Start() {
        var players = FindObjectsOfType<PlayerMovement>();
        player1 = players[0];
        player2 = players[1];
        animationPlayer.Play("Idle");
    }

    void Update() {
        var closest = FindClosestPlayer();
        var toClosest = closest.Position - Position;
        if (toClosest.magnitude > aggroDistance) {
            moveDir = Vector2.MoveTowards(moveDir, default(Vector2), Time.deltaTime * 3f);
            animationPlayer.EnsurePlaying("Idle");
            if(stepSound.isPlaying)
                stepSound.Stop();
        }
        else {
            moveDir = toClosest.normalized;
            animationPlayer.EnsurePlaying("WalkUp");
            if(!stepSound.isPlaying)
                stepSound.Play();
        }
    }

    private void FixedUpdate() {
        rb.velocity = moveDir * speed;
    }

    private PlayerMovement FindClosestPlayer() {
        var dist1 = Vector2.Distance(Position, player1.Position);
        var dist2 = Vector2.Distance(Position, player2.Position);

        return dist1 < dist2 ? player1 : player2;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null) {
            GameOver.DoGameOver();
            enabled = false;
            rb.velocity = Vector2.zero;
        }
    }
}