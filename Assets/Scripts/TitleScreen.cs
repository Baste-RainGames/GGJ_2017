using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class TitleScreen : MonoBehaviour {

    float orgIntensity;
    BloomModel.Settings bloom;

    private void Start() {
        bloom = GetComponent<PostProcessingBehaviour>().profile.bloom.settings;
        orgIntensity = 3;

        Invoke(nameof(Flicker), 1);
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Level1");
        }
    }


    void Flicker() {
        bloom.bloom.intensity = 5f;
        GetComponent<PostProcessingBehaviour>().profile.bloom.settings = bloom;
        Invoke(nameof(Restart), Random.Range(.1f, .3f));
    }

    void Restart() {
        StopAllCoroutines();
        StartCoroutine(ResetLight());
        Invoke(nameof(Flicker), Random.Range(1f, 3f));
    }

    IEnumerator ResetLight() {
        bloom.bloom.intensity = 0f;// orgIntensity * .25f;
        GetComponent<PostProcessingBehaviour>().profile.bloom.settings = bloom;
        while (true) {
            bloom.bloom.intensity = Mathf.Lerp(bloom.bloom.intensity, orgIntensity, .2f);
            GetComponent<PostProcessingBehaviour>().profile.bloom.settings = bloom;
            yield return new WaitForSeconds(.01f);
        }

    }
}