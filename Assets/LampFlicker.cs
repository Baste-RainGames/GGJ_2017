using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampFlicker : MonoBehaviour {

    float orgIntensity;
    Light light;

	// Use this for initialization
	void Start () {
        light = transform.GetChild(0).GetComponent<Light>();
        orgIntensity = light.intensity;

        Invoke("Flicker", Random.Range(2f, 5f));
	}
	
	void Flicker() {
        light.intensity = Random.Range(orgIntensity * .25f, orgIntensity * .4f);

        Invoke("Restart", Random.Range(.05f, .1f));
    }

    void Restart() {
        //light.intensity = orgIntensity;
        StopAllCoroutines();
        StartCoroutine(ResetLight());
        Invoke("Flicker", Random.Range(2f, 5f));
    }

    IEnumerator ResetLight() {
        while (true) {
            light.intensity = Mathf.Lerp(light.intensity, orgIntensity, .18f);
            yield return new WaitForSeconds(.01f);
        }
            
    }
}
