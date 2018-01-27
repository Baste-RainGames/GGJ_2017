using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public PlayerID playerId;
    public KeyBinding keyBinding;

    private KeyCode upKey, downKey, leftKey, rightKey;
    private KeyCode shootKey;
    private KeyCode blinkKey;
    private KeyCode stealGunKey;
    private KeyCode stealEyesKey;
    private KeyCode stealBlinkKey;

    public PlayerInput otherPlayer;
    public SpriteRenderer gunRenderer;
    private PlayerMovement movementAgent;
    private Gun gun;
    private Blinker blinker;

    private Vector2 movementInput;

    [SerializeField]
    private bool hasGun;
    public bool HasGun {
        get { return hasGun; }
        private set {
            hasGun = value;
            gunRenderer.enabled = value;
        }
    }

    [SerializeField]
    private bool hasEyes;
    public bool HasEyes {
        get { return hasEyes; }
        set {
            hasEyes = value;
            if (value)
                EyesController.SetPlayerThatHasEyes(playerId);
        }
    }

    [SerializeField]
    private bool hasBlink;
    public bool HasBlink { get { return hasBlink; } set { hasBlink = value; } }

    void Awake() {
        movementAgent = GetComponent<PlayerMovement>();
        gun           = GetComponent<Gun>();
        blinker       = GetComponent<Blinker>();

        upKey         = keyBinding.moveUp;
        downKey       = keyBinding.moveDown;
        leftKey       = keyBinding.moveLeft;
        rightKey      = keyBinding.moveRight;
        shootKey      = keyBinding.shoot;
        blinkKey      = keyBinding.blink;
        stealGunKey   = keyBinding.stealGun;
        stealEyesKey  = keyBinding.stealEyes;
        stealBlinkKey =  keyBinding.stealBlink;

        //eh
        HasGun = hasGun;
        HasEyes = hasEyes;

        if (otherPlayer == null) {
            var allPlayers = FindObjectsOfType<PlayerInput>();
            if (allPlayers.Length > 1) {
                otherPlayer = allPlayers[0] == this ? allPlayers[1] : allPlayers[0];
            }
        }

        if (otherPlayer != null && otherPlayer.playerId == playerId) {
            Debug.LogError("SAME PLAYER ID ON PLAYERS OMG");
        }

        if (gunRenderer == null) {
            var directionIndicator = transform.Find("DirectionIndicator");
            if (directionIndicator != null)
                gunRenderer = directionIndicator.GetComponent<SpriteRenderer>();
        }
    }

    void Update() {
        MovementInput();

        if (hasGun) {
            ShootInput();
        }
        else if (Input.GetKeyDown(stealGunKey)) {
            otherPlayer.HasGun = false;
            HasGun = true;
        }

        if (!hasEyes && Input.GetKeyDown(stealEyesKey)) {
            otherPlayer.HasEyes = false;
            HasEyes = true;
        }

        if (HasBlink) {
            BlinkInput();
        }
        else if (Input.GetKeyDown(stealBlinkKey)) {
            otherPlayer.HasBlink = false;
            HasBlink = true;
        }
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

    private void ShootInput() {
        if (Input.GetKeyDown(shootKey))
            gun.StartShooting();

        if (Input.GetKeyUp(shootKey))
            gun.StopShooting();
    }

    private void BlinkInput() {
        if (Input.GetKeyDown(blinkKey))
            blinker.TryToBlink();
    }

    private void OnDisable() {
        gun.StopShooting();
    }
}