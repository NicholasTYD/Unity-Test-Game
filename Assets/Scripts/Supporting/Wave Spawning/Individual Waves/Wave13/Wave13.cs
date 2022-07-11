using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave13 : Wave
{
    [SerializeField] List<GameObject> sw7Enemies;

    protected override IEnumerator sw1()
    {
        spawn(EnemyList.Instance.Shaman, pos(0));
        spawn(EnemyList.Instance.Tengu,  pos(1));
        spawn(EnemyList.Instance.Tengu, pos(2));

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw2());
    }

    IEnumerator sw2()
    {
        spawn(EnemyList.Instance.Sniper, pos(2));

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw3());
    }

    IEnumerator sw3()
    { 
        spawn(EnemyList.Instance.Sniper, pos(3));
        spawn(EnemyList.Instance.Tengu, pos(3));

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw4());
    }

    IEnumerator sw4()
    {
        spawn(EnemyList.Instance.Sniper, pos(4));
        spawn(EnemyList.Instance.Tengu, pos(4));
        spawn(EnemyList.Instance.Shaman, pos(4));

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw5());
    }

    IEnumerator sw5()
    {
        spawn(EnemyList.Instance.Sniper, pos(5));
        spawn(EnemyList.Instance.Tengu, pos(5));
        spawn(EnemyList.Instance.Hntrss, pos(5));

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw6());
    }

    IEnumerator sw6()
    {
        spawn(EnemyList.Instance.Sniper, pos(6));
        spawn(EnemyList.Instance.Tengu, pos(6));
        spawn(EnemyList.Instance.RedWzd, pos(6));
        spawn(EnemyList.Instance.PpeWzd, pos(6));

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw7());
    }

    IEnumerator sw7()
    {
        spawn(EnemyList.Instance.Sniper, pos(1));
        spawn(EnemyList.Instance.Sniper, pos(2));

        spawn(EnemyList.Instance.Hntrss);

        yield return null;
        StartCoroutine(concludeWaveOnKill());
    }
}
