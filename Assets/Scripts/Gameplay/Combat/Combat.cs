using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityMain))]
public abstract class Combat : MonoBehaviour
{
    protected EntityMain entityMain;
    [SerializeField] protected float attack;

    protected virtual void Start()
    {
        this.entityMain = this.GetComponent<EntityMain>();
        this.attack = entityMain.GetBaseAttack();
    }

    public abstract void Attack();

    public float GetAttack()
    {
        return attack;
    }

    public void SetAttack(float attack)
    {
        this.attack = attack;
    }
}
