using UnityEngine;

public class Destructible : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.HasComponent<Bullet>()) {
            Destroy(gameObject);
        }
    }
}