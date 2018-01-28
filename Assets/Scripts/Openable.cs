using System.Collections;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Openable : MonoBehaviour {

    public Collider2D doorCollider;

    public void Open() {

        StartCoroutine(MoveDown());
        
    }

    IEnumerator MoveDown() {
        while(transform.position.z < 2) {
            transform.position -= Vector3.back * .1f;
            yield return new WaitForSeconds(.01f);
        }
        doorCollider.enabled = false;
    }

}