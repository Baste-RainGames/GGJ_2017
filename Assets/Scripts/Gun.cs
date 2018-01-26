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
        bool shotBlocked;
        var spawnPos = FindBulletSpawnPosition(direction, out shotBlocked);
        if (shotBlocked)
            return;
        var bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity).GetComponent<Bullet>();
        bullet.Fire(direction);
        timeOfLastShot = Time.time;
    }

    private Vector2 FindBulletSpawnPosition(Vector2 direction, out bool shotBlocked) {
        var raycastHit = Physics2D.Raycast(transform.position, direction, 2f, 1 << LayerMask.NameToLayer("Default"));
        if (raycastHit) {
            Debug.Log(raycastHit.distance);
            if (raycastHit.distance < .5f) {
                Debug.Log("hit " + raycastHit.collider.name);
                shotBlocked = true;
                return default(Vector2);
            }

            shotBlocked = false;
            return raycastHit.point;
        }


        shotBlocked = false;
        return (Vector2) transform.position + direction;
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