using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    PlayerMovement playerMovement;

    Vector3 orgPos;
    Vector3 targetPos;
	// Use this for initialization
	void Start () {
        playerMovement = transform.parent.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {

        if (playerMovement.movementVector.magnitude != 0) {
        }
            
    }
}
