using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnvironmentalHazard", menuName = "ScriptableObjects/EnvironmentalHazard/BaseStats")]
public class EnvHazardBasicStatsScriptableObject : ScriptableObject
{
    public GameObject Prefab;
    public float Damage;
    public float Lifetime;
}
