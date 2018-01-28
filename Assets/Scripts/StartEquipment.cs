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
        player.HasTeleport = id == startsWithBlink;
    }

    public void Apply(PlayerInput player, bool levelHasGun, bool levelHasEyes, bool levelHasTeleport) {
        var id = player.playerId;
        player.HasGun   = id == startsWithGun && levelHasGun;
        player.HasEyes  = id == startsWithEyes && levelHasEyes;
        player.HasTeleport = id == startsWithBlink && levelHasTeleport;
    }
}