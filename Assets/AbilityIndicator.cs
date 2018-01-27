using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIndicator : MonoBehaviour {

    public Image gunBG;
    public Image seeBG;
    public Image blinkBG;

    public void Set(Ability ability, bool value) {
        Image image = null;
        switch (ability) {
            case Ability.Gun:
                image = gunBG;
                break;
            case Ability.See:
                image = seeBG;
                break;
            case Ability.Blink:
                image = blinkBG;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(ability), ability, null);
        }

        image.color = value ? Color.green : Color.red;
    }
}

public enum Ability {
    Gun,
    See,
    Blink
}