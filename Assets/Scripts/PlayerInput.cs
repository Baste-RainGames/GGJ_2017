using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public PlayerID playerId;
    public KeyBinding keyBinding;
    public StartEquipment startEquipment;
    public AudioSource stealAbilitySound;

    private KeyCode upKey, downKey, leftKey, rightKey;
    private KeyCode shootKey;
    private KeyCode blinkKey;
    private KeyCode stealGunKey;
    private KeyCode stealEyesKey;
    private KeyCode stealBlinkKey;

    public PlayerInput otherPlayer;
    private PlayerMovement movementAgent;
    private Gun gun;
    private Blinker blinker;
    private AbilityIndicator abilityIndicator;

    private Vector2 movementInput;
    private LevelSetup levelSetup;

    private bool levelHasGun, levelHasTeleport, levelHasEyes;
    private bool hasRunStart;

    private bool hasGun;
    public bool HasGun {
        get { return hasGun; }
        set {
            var old = hasGun;
            hasGun = value && levelHasGun;
            GetComponentInChildren<Animator>().SetLayerWeight(1, hasGun ? 1 : 0);
            abilityIndicator.Set(Ability.Gun, hasGun);

            if (hasGun != old && hasRunStart) {
                stealAbilitySound.Play();
            }
        }
    }

    private bool hasEyes;
    public bool HasEyes {
        get { return hasEyes; }
        set {
            var old = hasEyes;
            hasEyes = value && levelHasEyes;
            abilityIndicator.Set(Ability.See, hasEyes);
            if (hasEyes || !levelHasEyes) {
                EyesController.SetPlayerThatHasEyes(playerId, levelHasEyes);
            }

            if (hasEyes != old && hasRunStart) {
                stealAbilitySound.Play();
            }
        }
    }

    private bool hasTeleport;
    public bool HasTeleport {
        get { return hasTeleport; }
        set {
            var old = hasTeleport;
            hasTeleport = value && levelHasTeleport;
            abilityIndicator.Set(Ability.Blink, hasTeleport);

            if (hasTeleport != old && hasRunStart) {
                stealAbilitySound.Play();
            }
        }
    }

    void Awake() {
        movementAgent    = GetComponent<PlayerMovement>();
        gun              = GetComponent<Gun>();
        blinker          = GetComponent<Blinker>();
        abilityIndicator = GetComponentInChildren<AbilityIndicator>();

        levelSetup = FindObjectOfType<LevelSetup>();
        if (levelSetup) {
            levelHasTeleport = levelSetup.teleportActive;
            levelHasEyes     = levelSetup.sightActive;
            levelHasGun      = levelSetup.gunActive;
        }
        else {
            levelHasTeleport = levelHasEyes = levelHasGun = true;
        }

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
    }

    private void Start() {
        startEquipment.Apply(this, levelHasGun, levelHasEyes, levelHasTeleport);
        hasRunStart = true;
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

        if (HasTeleport) {
            BlinkInput();
        }
        else if (Input.GetKeyDown(stealBlinkKey)) {
            otherPlayer.HasTeleport = false;
            HasTeleport = true;
        }
    }

    private void MovementInput() {
        movementInput = Vector2.zero;
        if (keyBinding.useAxis) {
            movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else {
            if (Input.GetKey(upKey))
                movementInput += Vector2.up;
            if (Input.GetKey(leftKey))
                movementInput += Vector2.left;
            if (Input.GetKey(downKey))
                movementInput += Vector2.down;
            if (Input.GetKey(rightKey))
                movementInput += Vector2.right;
        }

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