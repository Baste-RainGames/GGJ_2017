using UnityEngine;

[CreateAssetMenu]
public class KeyBinding : ScriptableObject {
    public bool useAxis = false;
    public KeyCode moveUp     = KeyCode.W;
    public KeyCode moveLeft   = KeyCode.A;
    public KeyCode moveDown   = KeyCode.S;
    public KeyCode moveRight  = KeyCode.D;
    public KeyCode shoot      = KeyCode.Space;
    public KeyCode blink      = KeyCode.E;
    public KeyCode stealGun   = KeyCode.Alpha1;
    public KeyCode stealEyes  = KeyCode.Alpha2;
    public KeyCode stealBlink = KeyCode.Alpha3;
}