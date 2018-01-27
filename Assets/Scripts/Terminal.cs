using UnityEngine;

public class Terminal : MonoBehaviour, IDamageable {

    public Openable[] openables;

    public void Damaged(int damage) {
        foreach (var openable in openables) {
            openable.Open();
        }
    }
}