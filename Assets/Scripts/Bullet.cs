using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed;
    private Rigidbody2D rb;
    public int Damage = 1;

    public AudioClip HitEnemy;
    public AudioClip HitWall;
    public Vector3 Position => transform.position;

    public AudioSource audioSource;
    public Animator animator;

    public GameObject hitBulletEffect;

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
        var damageable = other.GetComponent<IDamageable>();
        if (damageable == null)  {
            if (other.gameObject.layer == LayerMask.NameToLayer("Geometry") || other.gameObject.layer == LayerMask.NameToLayer("Default")) {
                audioSource.clip = HitWall;
            }
        }
        else {
            audioSource.clip = HitEnemy;
            damageable.Damaged(Damage);
        }

        hitBulletEffect.transform.parent = null;
        //audio source and animator on hitbulleteffect
        audioSource.Play();
        animator.enabled = true;
        Destroy(gameObject);
        Destroy(hitBulletEffect, .5f);
    }

}