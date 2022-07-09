using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave10 : Wave
{
    protected override IEnumerator sw1()
    {
        spawn(EnemyList.Instance.Prts, pos(0));

        yield return null;
        StartCoroutine(concludeBossWaveOnKill());
    }
}
