using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovementAI : MonoBehaviour
{
    public abstract bool StopCriteraFufilled();
    public abstract void Move();
}
