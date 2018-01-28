using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    PlayerMovement playerMovement;

    Vector3 orgPos;
    Vector3 targetPos;
    // Use this for initialization
    void Start () {
        orgPos = transform.localPosition;
        playerMovement = transform.parent.GetComponent<PlayerMovement>();
    }
    
    // Update is called once per frame
    void Update () {

        if (playerMovement.movementVector.magnitude != 0) {
            transform.localPosition = Vector3.Lerp(transform.localPosition, orgPos + new Vector3(playerMovement.movementVector.x, playerMovement.movementVector.y, 0), Time.deltaTime);
        }
            
    }
}
