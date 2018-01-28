using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAfterMovie : MonoBehaviour {

    IEnumerator Start() {
        yield return new WaitForSeconds(38f);
        LoadNextLevel();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel() {
        SceneManager.LoadScene("TitleScreen");
    }

}
