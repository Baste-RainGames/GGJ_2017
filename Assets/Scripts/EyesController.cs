using UnityEngine;

public class EyesController : MonoBehaviour {

    public static EyesController instance;

    private void Awake() {
        instance = this;
    }

    public Canvas sightBlockOverlayCanvas;
    public Camera player1Camera;
    public Camera player2Camera;

    public static void SetPlayerThatHasEyes(PlayerID player) {
        instance.sightBlockOverlayCanvas.worldCamera = player == PlayerID.One ? instance.player2Camera : instance.player1Camera;
    }

}