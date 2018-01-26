using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public KeyBinding keyBinding;
    private KeyCode upKey, downKey, leftKey, rightKey;
    private KeyCode shootKey;

    private KeyCode stealGunKey;
    
    [SerializeField]
    private bool hasGun;
    public bool HasGun {
        get { return hasGun; }
        set {
            hasGun = value;
            gunRenderer.enabled = value;
        }
    }

    public PlayerInput otherPlayer;
    public SpriteRenderer gunRenderer;

    private Vector2 movementInput;
    private PlayerMovement movementAgent;
    private Gun gun;

    void Awake() {
        movementAgent = GetComponent<PlayerMovement>();
        gun           = GetComponent<Gun>();

        upKey       = keyBinding.moveUp;
        downKey     = keyBinding.moveDown;
        leftKey     = keyBinding.moveLeft;
        rightKey    = keyBinding.moveRight;
        shootKey    = keyBinding.shoot;
        stealGunKey = keyBinding.stealGun;

        //eh
        HasGun = hasGun;

        if (otherPlayer == null) {
            var allPlayers = FindObjectsOfType<PlayerInput>();
            if (allPlayers.Length > 1) {
                otherPlayer = allPlayers[0] == this ? allPlayers[1] : allPlayers[0];
            }
        }

        if (gunRenderer == null) {
            var directionIndicator = transform.Find("DirectionIndicator");
            if(directionIndicator != null)
                gunRenderer = directionIndicator.GetComponent<SpriteRenderer>();
        }
    }

    void Update() {
        MovementInput();

        if (hasGun) {
            ShootInput();
        }
        else if (Input.GetKeyDown(stealGunKey)) {
            if (!otherPlayer.hasGun) {
                Debug.Log("ERROR ERROR NOBODY HAS GUN");
            }
            otherPlayer.HasGun = false;
            HasGun = true;
        }
    }

    private void ShootInput() {
        if (Input.GetKeyDown(shootKey))
            gun.StartShooting();

        if (Input.GetKeyUp(shootKey))
            gun.StopShooting();
    }

    private void MovementInput() {
        movementInput = Vector2.zero;
        if (Input.GetKey(upKey))
            movementInput += Vector2.up;
        if (Input.GetKey(leftKey))
            movementInput += Vector2.left;
        if (Input.GetKey(downKey))
            movementInput += Vector2.down;
        if (Input.GetKey(rightKey))
            movementInput += Vector2.right;

        if (movementInput == Vector2.zero)
            return;

        movementInput = movementInput.normalized;
        movementAgent.MoveInDir(movementInput);
    }
}