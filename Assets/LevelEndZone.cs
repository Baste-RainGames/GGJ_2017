using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndZone : MonoBehaviour {

    private bool p1Inside, p2Inside;
    public string nextLevel;
    private bool isLoadingLevel;

    public bool startSurvivalMode;

    public static int numKilled;
    public int numToKill = 50;
    public Spawner[] spawners;
    public TMP_Text killText;

    private void Awake() {
        numKilled = 0;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<PlayerInput>();
        if (player != null) {
            if (player.playerId == PlayerID.One)
                p1Inside = true;
            else
                p2Inside = true;
        }

        if (p1Inside && p2Inside && !isLoadingLevel) {
            if (!startSurvivalMode) {
                GameOver.ShowNextLevelScreen();
                StartCoroutine(LoadNextLevel());
            }
            else {
                StartCoroutine(GAUNTLET());
            }
        }
    }

    private IEnumerator GAUNTLET() {
        isLoadingLevel = true;
        GameOver.Show("SURVIVE");
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2f);

        foreach (var spawner in spawners) {
            spawner.enabled = true;
            spawner.registerKills = true;
        }

        GameOver.Hide();

        while (numToKill > numKilled) {
            killText.text = $"{numKilled} killed, {Mathf.Max(0, numToKill - numKilled)} remaining!";
            yield return null;
        }
        killText.text = $"{numKilled} killed, {Mathf.Max(0, numToKill - numKilled)} remaining!";

        GameOver.ShowNextLevelScreen();
        StartCoroutine(LoadNextLevel());
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
