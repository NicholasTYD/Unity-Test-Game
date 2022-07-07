using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardDropper : MonoBehaviour
{
    [SerializeField] EnvHazardBasicStatsScriptableObject envHazard;
    [SerializeField] float spawnInterval;

    private void Start()
    {
        StartCoroutine(dropHazard());
    }

    IEnumerator dropHazard()
    {
        while (true)
        {
            CombatMechanics.Instance.InstantiateEnvHazard(envHazard.Prefab, 
                this.transform.position, envHazard.Damage, envHazard.Lifetime);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
