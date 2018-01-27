using UnityEngine;

[CreateAssetMenu]
public class StartEquipment : ScriptableObject {
    public PlayerID startsWithGun;
    public PlayerID startsWithEyes;
    public PlayerID startsWithBlink;

    public void Apply(PlayerInput player) {
        var id = player.playerId;
        player.HasGun   = id == startsWithGun;
        player.HasEyes  = id == startsWithEyes;
        player.HasBlink = id == startsWithBlink;
    }
}