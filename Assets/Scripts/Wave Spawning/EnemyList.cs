using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public static EnemyList Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public GameObject SprSke;
    public GameObject SwdSke;
    public GameObject Archer;
    public GameObject Pile;
    public GameObject FreKgt;

    public GameObject Witch;
    public GameObject RedWzd;
    public GameObject PpeWzd;
    public GameObject Hntrss;
    public GameObject Prts;

    public GameObject Tengu;
    public GameObject Shaman;
    public GameObject Sniper;
    public GameObject Samurai;
}
