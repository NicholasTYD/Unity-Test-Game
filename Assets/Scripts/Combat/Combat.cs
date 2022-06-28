using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityMain))]
public abstract class Combat : MonoBehaviour
{
    protected EntityMain entityMain;
    [SerializeField] protected float baseAttack;

    protected virtual void Start()
    {
        this.entityMain = this.GetComponent<EntityMain>();
        this.baseAttack = entityMain.GetBaseAttack();
    }

    public abstract void Attack();
}
