using UnityEngine;

public class Destructible : MonoBehaviour {

    public int Health = 3;

    private void OnCollisionEnter2D(Collision2D other) {
        var bullet = other.GetComponent<Bullet>();
        if (bullet != null) {
            bullet.OnDamagedObject();
            Health -= bullet.Damage;

            if(Health <= 0)
                Destroy(gameObject);
        }
    }
}