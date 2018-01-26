using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public PlayerID playerId;
    public KeyBinding keyBinding;
    private KeyCode upKey, downKey, leftKey, rightKey;
    private KeyCode shootKey;

    private KeyCode stealGunKey;
    private KeyCode stealEyesKey; 
    
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
        get {
            return hasEyes;
        }
        set {
            hasEyes = value;
            if(value)
                EyesController.SetPlayerThatHasEyes(playerId);
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

        upKey        = keyBinding.moveUp;
        downKey      = keyBinding.moveDown;
        leftKey      = keyBinding.moveLeft;
        rightKey     = keyBinding.moveRight;
        shootKey     = keyBinding.shoot;
        stealGunKey  = keyBinding.stealGun;
        stealEyesKey = keyBinding.stealEyes;

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
            if (!otherPlayer.HasGun) {
                Debug.Log("ERROR ERROR NOBODY HAS GUN");
            }
            otherPlayer.HasGun = false;
            HasGun = true;
        }

        if (!hasEyes && Input.GetKeyDown(stealEyesKey)) {
            otherPlayer.HasEyes = false;
            HasEyes = true;
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

    private void OnDisable() {
        gun.StopShooting();
    }
}