using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour {

    public AudioClip clip;

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Play();
        }
    }

    public void Play() {
        SceneManager.LoadScene("Level1");
        MainSoundHandler.PlayClip(clip);
    }
}