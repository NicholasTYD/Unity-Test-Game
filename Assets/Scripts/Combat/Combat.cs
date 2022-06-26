using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combat : MonoBehaviour
{
    protected float baseAttack;

    public abstract void Attack();
}
