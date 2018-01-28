using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform gunZPos;
    public float shootCooldown = .5f;

    public AudioSource source;
    public AudioClip gunSound;

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
        source.PlayOneShot(gunSound);
        bullet.Fire(direction);
        timeOfLastShot = Time.time;
    }

    private Vector3 FindBulletSpawnPosition(Vector2 direction, out bool shotBlocked) {
        var raycastHit = Physics2D.Raycast(transform.position, direction, 2f, Layers.GeometryMaskWithoutBlink);
        if (raycastHit) {
            if (raycastHit.distance < .5f) {
                shotBlocked = true;
                return default(Vector3);
            }

            shotBlocked = false;
            return new Vector3(raycastHit.point.x, raycastHit.point.y, gunZPos.position.z);
        }


        shotBlocked = false;
        var pos = (Vector2) transform.position + direction;
        return new Vector3(pos.x, pos.y, gunZPos.position.z);
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