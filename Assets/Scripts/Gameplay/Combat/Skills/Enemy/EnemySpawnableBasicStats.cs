using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnableBasicStats", menuName = "ScriptableObjects/Enemy/EnemySpawnableBasicStats")]
public class EnemySpawnableBasicStats : ScriptableObject
{
    public GameObject Spawnable;
    public float Speed;
    public float Lifetime;
}
