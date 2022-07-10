using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int CurrentWave;

    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public float Attack { get; set; }
    public float AttackSpeed { get; set; }
    public float ParryDamageBonusDuration { get; set; }
    public float ParryDamageBonusMultiplier { get; set; }
    public float MovementSpeed { get; set; }
    public float MaxComboTime { get; set; }
    public bool ParryStrikeUnlocked { get; set; }
}
