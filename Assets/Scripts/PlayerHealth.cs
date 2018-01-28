using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public int health = 5;
    public Sprite hasHealth, noHealth;

    public Image[] healths;

    public void GetDamaged() {
        health -= 1;
        if(health <= 0)
            GameOver.DoGameOver();

        for (int i = 0; i < healths.Length; i++) {
            healths[i].sprite = (i >= health) ? noHealth : hasHealth;
        }
    }

}