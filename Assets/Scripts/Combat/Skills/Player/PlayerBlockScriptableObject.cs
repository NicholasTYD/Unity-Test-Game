using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBlock", menuName = "ScriptableObjects/Player/PlayerBlock")]
public class PlayerBlockScriptableObject : ScriptableObject
{
    // Name of trigger for block
    public new string name;
    public string parryName;

    // How long a single frame lasts in the animation
    public float blockDurationPerFrame;
    // How long the entire animation lasts.
    public float baseBlockDuration;

    public float parryDurationPerFrame;
    public float baseParryDuration;
    public float parryBonusDamageMultiplier;

    public Vector2 hurtboxCenterOffset;
    public float hurtboxRadius;
}
