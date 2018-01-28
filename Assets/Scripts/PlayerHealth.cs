using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public int health = 5;
    public Sprite hasHealth, noHealth;
    public PlayerMovement playerMovement;

    public Image[] healths;

    public void GetDamaged() {
        if (health <= 0)
            return;
        health -= 1;
        if (health <= 0) {
            playerMovement.animationPlayer.UnLock("Damaged");
            playerMovement.animationPlayer.LockInto("Dead");
            GameOver.DoGameOver();
        }
        else {
            playerMovement.StartCoroutine(playerMovement.DoDamageReaction());
        }

        for (int i = 0; i < healths.Length; i++) {
            healths[i].sprite = (i >= health) ? noHealth : hasHealth;
        }
    }
}