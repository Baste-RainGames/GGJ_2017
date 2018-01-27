using System.Collections.Generic;
using UnityEngine;

public class WalkOnTopChecker : MonoBehaviour {

    private PlayerMovement playerMovement;

    private HashSet<Collider2D> touching = new HashSet<Collider2D>();

    private void Awake() {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        playerMovement.UpdateZPos(-0.1875f);
        touching.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other) {
        touching.Remove(other);
        if (touching.Count == 0) {
            playerMovement.ReturnToDefaultZPos();
        }
    }

}
