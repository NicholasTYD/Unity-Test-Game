using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave/Info")]
public class WaveInfoScriptableObject : ScriptableObject
{
    public string waveName;
    public AudioClip waveBGM;
    public List<Vector2> notableSpawnPos;
}
