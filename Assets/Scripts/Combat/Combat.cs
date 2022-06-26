using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combat : MonoBehaviour
{
    [SerializeField] protected float baseAttack;

    public abstract void Attack();
}
