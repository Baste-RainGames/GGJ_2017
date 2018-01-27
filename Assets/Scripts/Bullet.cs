using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed;
    private Rigidbody2D rb;
    public int Damage = 1;

    public AudioClip HitEnemy;
    public AudioClip HitWall;
    public AudioSource audioSource;
    public Vector3 Position => transform.position;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator Start() {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void Fire(Vector2 direction) {
        rb.velocity = direction.normalized * bulletSpeed;
        rb.angularVelocity = 1500f;
    }

    
    private void OnCollisionEnter2D(Collision2D other) {
        var damageable = other.GetComponent<Destructible>();
        if (damageable == null)  {
            if (other.gameObject.layer == LayerMask.NameToLayer("Geometry")) {
                audioSource.clip = HitWall;
                audioSource.Play();
            }
        }
        else {
            OnDamagedObject();
            damageable.Health -= Damage;

            if (damageable.Health <= 0)
                damageable.Destroy();
        }

        Destroy(GetComponent<Collider>());
        Destroy(this);
        Destroy(gameObject, .5f);
    }

    public void OnDamagedObject() {
        GetComponent<SpriteRenderer>().color = Color.red;
        audioSource.clip = HitEnemy;
        audioSource.Play();
    }
}