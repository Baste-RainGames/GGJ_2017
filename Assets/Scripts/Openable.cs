using UnityEngine;
using UnityEngine.PostProcessing;

public class Openable : MonoBehaviour {

    public Collider2D doorCollider;

    public void Open() {
        var an = GetComponent<Animator>();
        if (an != null) {
            an.Play("Open");
        }
        else {
            Destroy(gameObject);
        }

        doorCollider.enabled = false;
    }

}