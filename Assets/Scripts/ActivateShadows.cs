﻿using UnityEngine;

public class ActivateShadows : MonoBehaviour {

    void Start () {
        GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        GetComponent<Renderer>().receiveShadows = true;
    }
}
