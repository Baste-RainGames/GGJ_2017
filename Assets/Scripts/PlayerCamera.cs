using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    PlayerMovement playerMovement;

    Quaternion orgRotation;
    Quaternion targetRotation;
	// Use this for initialization
	void Start () {
        playerMovement = transform.parent.GetComponent<PlayerMovement>();
        orgRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

        if (playerMovement.movementVector.magnitude != 0)
            targetRotation = orgRotation * Quaternion.Euler(-playerMovement.movementVector.y * 2, playerMovement.movementVector.x * 2, 0);

        else
            targetRotation = orgRotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 3f * Time.deltaTime);
    }
}
