using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public KeyCode upKey, downKey, leftKey, rightKey;
    public KeyCode shootKey;

    private Vector2 movementInput;
    private PlayerMovement movementAgent;
    private Gun gun;

    void Awake() {
        movementAgent = GetComponent<PlayerMovement>();
        gun           = GetComponent<Gun>();
    }

    void Update() {
        MovementInput();
        ShootInput();
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