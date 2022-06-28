using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseStats", menuName = "ScriptableObjects/Main/BaseStats")]
public class BaseStatsScriptableObject : ScriptableObject
{
    public float BaseMaxHealth;
    public float BaseAttack;
    public float BaseMovementSpeed;
}
