using System.Collections.Generic;
using UnityEngine;

public class WalkOnTopChecker : MonoBehaviour {

    public Transform thingToMoveZPosOf;
    private HashSet<Collider2D> touching = new HashSet<Collider2D>();

    private void OnTriggerEnter2D(Collider2D other) {
        thingToMoveZPosOf.position = thingToMoveZPosOf.position.WithZ(-0.1875f);
        touching.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other) {
        touching.Remove(other);
        if (touching.Count == 0) {
            thingToMoveZPosOf.position = thingToMoveZPosOf.position.WithZ(0f);
        }
    }

}
