using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public PlayerID playerId;
    public KeyBinding keyBinding;
    public StartEquipment startEquipment;

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
    private AbilityIndicator abilityIndicator;

    private Vector2 movementInput;

    private bool hasGun;
    public bool HasGun {
        get { return hasGun; }
        set {
            hasGun = value;
            gunRenderer.enabled = value;
            GetComponentInChildren<Animator>().SetLayerWeight(1, value ? 1 : 0);
            abilityIndicator.Set(Ability.Gun, value);
        }
    }

    private bool hasEyes;
    public bool HasEyes {
        get { return hasEyes; }
        set {
            hasEyes = value;
            abilityIndicator.Set(Ability.See, value);
            if (value) {
                EyesController.SetPlayerThatHasEyes(playerId);
            }
        }
    }

    private bool hasBlink;
    public bool HasBlink {
        get {
            return hasBlink;
        }
        set {
            hasBlink = value;
            abilityIndicator.Set(Ability.Blink, value);
        }
    }

    void Awake() {
        movementAgent = GetComponent<PlayerMovement>();
        gun           = GetComponent<Gun>();
        blinker       = GetComponent<Blinker>();
        abilityIndicator = GetComponentInChildren<AbilityIndicator>();

        upKey         = keyBinding.moveUp;
        downKey       = keyBinding.moveDown;
        leftKey       = keyBinding.moveLeft;
        rightKey      = keyBinding.moveRight;
        shootKey      = keyBinding.shoot;
        blinkKey      = keyBinding.blink;
        stealGunKey   = keyBinding.stealGun;
        stealEyesKey  = keyBinding.stealEyes;
        stealBlinkKey = keyBinding.stealBlink;

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

    private void Start() {
        startEquipment.Apply(this);
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