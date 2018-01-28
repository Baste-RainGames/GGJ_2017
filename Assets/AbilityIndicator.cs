using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIndicator : MonoBehaviour {

    public Image gunBG;
    public Image seeBG;
    public Image blinkBG;

    private Sprite hasGun, hasSee, hasBlink;
    public Sprite noGun, noSee, noBlink;

    void Awake() {
        hasGun = gunBG.sprite;
        hasSee = seeBG.sprite;
        hasBlink = blinkBG.sprite;
    }

    public void Set(Ability ability, bool value) {
        switch (ability) {
            case Ability.Gun:
                gunBG.sprite = value ? hasGun : noGun;
                break;
            case Ability.See:
                seeBG.sprite = value ? hasSee : noSee;
                break;
            case Ability.Blink:
                blinkBG.sprite = value ? hasBlink : noBlink;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(ability), ability, null);
        }
    }
}

public enum Ability {
    Gun,
    See,
    Blink
}