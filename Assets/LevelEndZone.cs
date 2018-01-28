using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndZone : MonoBehaviour {

    private bool p1Inside, p2Inside;
    public string nextLevel;
    private bool isLoadingLevel;

    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<PlayerInput>();
        if (player != null) {
            if (player.playerId == PlayerID.One)
                p1Inside = true;
            else
                p2Inside = true;
        }

        if (p1Inside && p2Inside && !isLoadingLevel) {
            GameOver.ShowNextLevelScreen();
            StartCoroutine(LoadNextLevel());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        var player = other.GetComponent<PlayerInput>();
        if (player != null) {
            if (player.playerId == PlayerID.One)
                p1Inside = false;
            else
                p2Inside = false;
        }
    }

    private IEnumerator LoadNextLevel() {
        isLoadingLevel = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextLevel);
    }

}
