using UnityEngine;

public class Destructible : MonoBehaviour, IDamageable {

    public int Health = 3;
    public AudioSource deathSound;

    public void Destroy() {
        if (deathSound != null) {
            deathSound.transform.parent = null;
            deathSound.Play();
            Destroy(deathSound.gameObject, deathSound.clip.length * 1.1f);
        }
        Destroy(gameObject);
    }

    public void Damaged(int damage) {
        Health -= damage;

        if (Health <= 0)
            Destroy();
    }
}