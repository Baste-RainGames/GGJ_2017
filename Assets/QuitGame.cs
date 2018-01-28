using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour {

    public AudioClip clip;

    public void Quit() {
        MainSoundHandler.PlayClip(clip);

        StartCoroutine(QuitSoon());
    }

    private IEnumerator QuitSoon() {
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}