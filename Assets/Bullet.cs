using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector2 direction) {
        rb.velocity = direction.normalized * bulletSpeed;
        rb.angularVelocity = 1500f;
    }
}