using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public int health = 5;

    public void GetDamaged() {
        health -= 1;
        if(health <= 1)
            GameOver.DoGameOver();
    }

}