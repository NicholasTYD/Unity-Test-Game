using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRoll", menuName = "ScriptableObjects/Player/PlayerRoll")]
public class PlayerRollScriptableObject : ScriptableObject
{
    // Name of trigger for roll
    public new string name;
    // How long the entire animation lasts.
    public float rollDuration;
    public float rollCooldown;
}
