using UnityEngine;
using UnityEngine.UI;

public class EyesController : MonoBehaviour {

    public static EyesController instance;

    private void Awake() {
        instance = this;
        player1Camera = GameObject.Find("Player 1 Camera UI").GetComponent<Camera>();
        player2Camera = GameObject.Find("Player 2 Camera UI").GetComponent<Camera>();
    }

    public Canvas sightBlockOverlayCanvas;
    public Camera player1Camera;
    public Camera player2Camera;

    public static void SetPlayerThatHasEyes(PlayerID player, bool levelHasEyes) {
        instance.sightBlockOverlayCanvas.worldCamera = player == PlayerID.One ? instance.player2Camera : instance.player1Camera;
        if (!levelHasEyes) {
            instance.sightBlockOverlayCanvas.GetComponentInChildren<Image>().enabled = false;
        }
    }

}