using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBasicAttack", menuName = "ScriptableObjects/Player/PlayerBasicAttack")]
public class PlayerBasicAttackScriptableObject : ScriptableObject
{
    // Name of trigger for attack
    public new string name;
    public float damageMultiplier;
    // How long a single frame lasts in the animation
    public float durationPerFrame;
    // How long the entire animation lasts.
    public float baseAttackDuration;
    // Time to wait before creating the hurtbox.
    public float timeBeforeHit;

    public Vector2 hurtboxCenterOffset;
    public float hurtboxRadius;
}
