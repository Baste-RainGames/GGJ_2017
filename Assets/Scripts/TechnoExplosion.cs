using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class TechnoExplosion : MonoBehaviour {
    public GameObject explosionTemplate;
    public AudioSource chargeSound;
    public AudioSource explodeSound;

    IEnumerator Start() {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        var animPlayer = new AnimationPlayer(GetComponentInChildren<Animator>());
        animPlayer.Play("ReadyExplosion");
        chargeSound.Play();

        yield return new WaitForSeconds(1f);

        animPlayer.Play("Explode");
        explodeSound.Play();

        explosionTemplate.SetActive(true);
        var radius = explosionTemplate.transform.localScale.x / 4f;
        var hitThings = Physics2D.OverlapCircleAll(transform.position, radius, 1 << LayerMask.NameToLayer("Player"));
        foreach (var hitThing in hitThings) {
            var player = hitThing.GetComponent<PlayerHealth>();
            if (player != null) {
                player.GetDamaged();
            }
        }

        yield return new WaitForSeconds(.5f);
        explosionTemplate.SetActive(false);
        yield return new WaitForSeconds(.5f);

        Destroy(gameObject);
    }
}