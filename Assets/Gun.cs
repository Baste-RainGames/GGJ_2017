using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject bulletPrefab;
    public float shootCooldown = .5f;

    private PlayerMovement movement;
    public bool IsShooting { get; private set; }
    private float timeOfLastShot;

    private void Awake() {
        movement = GetComponent<PlayerMovement>();
        timeOfLastShot = Time.time - shootCooldown;
    }

    void Update() {
        if (IsShooting && Time.time - timeOfLastShot > shootCooldown) {
            Fire();
        }
    }

    private void Fire() {
        var direction = FindShootDirection();
        var bullet = Instantiate(bulletPrefab, (Vector2) transform.position + direction, Quaternion.identity).GetComponent<Bullet>();
        bullet.Fire(direction);
        timeOfLastShot = Time.time;
    }

    private Vector2 FindShootDirection() {
        return movement.FacingDirection;
    }

    public void StartShooting() {
        IsShooting = true;
    }

    public void StopShooting() {
        IsShooting = false;
    }
}