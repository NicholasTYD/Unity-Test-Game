using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave5 : Wave
{
    protected override IEnumerator sw1()
    {
        spawn(EnemyList.Instance.FreKgt, pos(0));

        yield return null;
        StartCoroutine(concludeBossWaveOnKill());
    }
}
