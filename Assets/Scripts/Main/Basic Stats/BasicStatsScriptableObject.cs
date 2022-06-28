using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicStats", menuName = "ScriptableObjects/Main/BasicStats")]
public class BasicStatsScriptableObject : ScriptableObject
{
    public float BaseMaxHealth;
    public float BaseAttack;
    public float BaseMovementSpeed;
}
